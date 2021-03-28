using API.Dtos;
using AutoMapper;
using GetGroup.API.Errors;
using GetGroup.Core.Entities;
using GetGroup.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetGroup.API.Controllers
{

    public class HomeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
       
        public async Task<ActionResult<IReadOnlyList<Service>>> GetServices()
        {
            var services = await _unitOfWork.Repository<Service>().ListAllAsync();
            return Ok(services);
        }
    }
}
