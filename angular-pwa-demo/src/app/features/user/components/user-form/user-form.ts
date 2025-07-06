import { Component, OnInit, OnChanges, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

export interface User {
  id: number;
  name: string;
  email: string;
  age: number;
  phone: string;
  city: string;
}

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-form.html',
  styleUrls: ['./user-form.css']
})
export class UserFormComponent implements OnInit, OnChanges {
  @Input() user: User | null = null;
  @Input() isVisible = false;
  @Output() save = new EventEmitter<User>();
  @Output() close = new EventEmitter<void>();

  userForm!: FormGroup;
  isEditing = false;
  isSubmitting = false;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.initForm();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['user']) {
      if (this.user) {
        this.isEditing = true;
        setTimeout(() => {
          if (this.userForm && this.user) {
            this.userForm.patchValue(this.user);
          }
        });
      } else {
        this.isEditing = false;
        if (this.userForm) {
          this.userForm.reset();
        }
      }
    }
  }

  private initForm() {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      age: ['', [Validators.required, Validators.min(1), Validators.max(120)]],
      phone: ['', [Validators.required, Validators.minLength(10)]],
      city: ['', [Validators.required, Validators.minLength(2)]]
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      this.isSubmitting = true;
      
      const userData: User = {
        id: this.user?.id || this.generateId(),
        ...this.userForm.value
      };

      // Simular delay de guardado
      setTimeout(() => {
        this.save.emit(userData);
        this.isSubmitting = false;
        this.closeForm();
      }, 1000);
    } else {
      this.markFormGroupTouched();
    }
  }

  closeForm() {
    this.close.emit();
    this.userForm.reset();
    this.isEditing = false;
    this.isSubmitting = false;
  }

  private markFormGroupTouched() {
    Object.keys(this.userForm.controls).forEach(key => {
      const control = this.userForm.get(key);
      control?.markAsTouched();
    });
  }

  private generateId(): number {
    return Math.floor(Math.random() * 10000) + 1;
  }
} 