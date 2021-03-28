import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { IService } from 'src/app/shared/models/service';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];
  services: IService[];
  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
    this.getServicess();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      email: [null, 
        [Validators.required, Validators
        .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]
      ],
      password: [null, Validators.required],
      serviceId: ['', [Validators.required]]

    });
  }

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(response => {
      var userRole= localStorage.getItem('role')
      if(userRole=="Admin"){
        this.router.navigateByUrl("/admin");
       }
       else if(userRole=="User"){
        this.router.navigateByUrl("/request");
  
       }
    }, error => {
      console.log(error);
      this.errors = error.errors;
    })
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
               return res ? {emailExists: true} : null;
            })
          );
        })
      )
    }
  }

  getServicess() {
    this.accountService.getServices().subscribe(response => {
      this.services = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    })
  }

}


