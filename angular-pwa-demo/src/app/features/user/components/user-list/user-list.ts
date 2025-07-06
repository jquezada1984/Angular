import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from '../../../../shared/components/nav-bar/nav-bar';
import { UserFormComponent, User } from '../user-form/user-form';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, NavBarComponent, UserFormComponent],
  templateUrl: './user-list.html',
  styleUrls: ['../../../../shared/styles/list-styles.css']
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  showForm = false;
  selectedUser: User | null = null;

  ngOnInit() {
    // Datos de ejemplo
    this.users = [
      {
        id: 1,
        name: 'Juan Pérez',
        email: 'juan.perez@email.com',
        age: 28,
        phone: '+34 123 456 789',
        city: 'Madrid'
      },
      {
        id: 2,
        name: 'María García',
        email: 'maria.garcia@email.com',
        age: 32,
        phone: '+34 987 654 321',
        city: 'Barcelona'
      },
      {
        id: 3,
        name: 'Carlos López',
        email: 'carlos.lopez@email.com',
        age: 25,
        phone: '+34 555 123 456',
        city: 'Valencia'
      }
    ];
  }

  addUser() {
    this.selectedUser = null;
    this.showForm = true;
  }

  editUser(user: User) {
    this.selectedUser = { ...user };
    this.showForm = true;
  }

  deleteUser(userId: number) {
    if (confirm('¿Estás seguro de que quieres eliminar este usuario?')) {
      this.users = this.users.filter(user => user.id !== userId);
    }
  }

  onSaveUser(user: User) {
    if (this.selectedUser) {
      // Editar usuario existente
      const index = this.users.findIndex(u => u.id === user.id);
      if (index !== -1) {
        this.users[index] = user;
      }
    } else {
      // Agregar nuevo usuario
      this.users.push(user);
    }
  }

  onCloseForm() {
    this.showForm = false;
    this.selectedUser = null;
  }

  trackByUserId(index: number, user: User): number {
    return user.id;
  }
} 