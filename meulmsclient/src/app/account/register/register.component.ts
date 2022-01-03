import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm : any = FormGroup;
  selected = 'Student';

  title = "Ders Seçimi";
  
  courses = [
    { id: 1, name: "Bitirme Ödevi - 1" },
    { id: 2, name: "Image Processing" },
    { id: 3, name: "Psikolojiye Giriş" },
    { id: 4, name: "Machine Learning" },
    { id: 5, name: "Digital Control Systems" },
  ];
  
  public selectedItem = [{ id: 1, name: "Bitirme Ödevi - 1" }];

  constructor(private accountService:AccountService, private router:Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = new FormGroup({
      email : new FormControl('', [Validators.required, Validators.pattern('^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$')]),
      password: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      courses: new FormControl(this.selectedItem),
      role: new FormControl(this.selected),
    });
  }

  onSubmit(){
    this.registerForm.value.courses = this.selectedItem;
    this.registerForm.value.role = this.selected;
    console.log(this.registerForm.value);

    this.accountService.register(this.registerForm.value).subscribe(() => {
      this.router.navigateByUrl("/course")
    },
    error =>{
      console.log(error.error)
    })
  }

  

}
