using AutoMapper;
using Core.Specifications;
using GetGroup.API.Dtos;
using GetGroup.API.Errors;
using GetGroup.API.Helpers;
using GetGroup.Core.Entities;
using GetGroup.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetGroup.API.Controllers
{
 
 
    public class AdminController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminController( IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<UserServiceDto>>> GetServices(
       [FromQuery] ServiceSpecParams  serviceSpecParams)
        {
            var spec = new UserServiceSpecification(serviceSpecParams);
            var countSpec = new UserServiceSpecificationCount(serviceSpecParams);

            var totalItems = await _unitOfWork.Repository<UserService>().CountAsync(countSpec);

            var services = await _unitOfWork.Repository<UserService>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<UserServiceDto>>(services);

            return Ok(new Pagination<UserServiceDto>(serviceSpecParams.PageIndex,
                serviceSpecParams.PageSize, totalItems, data));
        }



        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserServiceDto>> GetProduct(int userId)
        {
            var spec = new UserServiceSpecification(userId);
            var service = await _unitOfWork.Repository<UserService>().GetEntityWithSpec(spec);
            if (service == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<UserServiceDto>(service);
        }
    }
}
