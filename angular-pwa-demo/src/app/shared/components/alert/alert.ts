import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

export type AlertType = 'success' | 'error' | 'warning' | 'info';

@Component({
  selector: 'app-alert',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="alert" [ngClass]="alertClass" *ngIf="show">
      <div class="alert-content">
        <span class="alert-icon">{{ getIcon() }}</span>
        <span class="alert-message">{{ message }}</span>
        <button 
          class="alert-close" 
          (click)="onClose()"
          *ngIf="dismissible">
          ×
        </button>
      </div>
    </div>
  `,
  styles: [`
    .alert {
      padding: 1rem;
      border-radius: 4px;
      margin-bottom: 1rem;
      border: 1px solid transparent;
    }
    
    .alert-content {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }
    
    .alert-icon {
      font-size: 1.2rem;
    }
    
    .alert-message {
      flex: 1;
    }
    
    .alert-close {
      background: none;
      border: none;
      font-size: 1.5rem;
      cursor: pointer;
      padding: 0;
      line-height: 1;
    }
    
    .alert-success {
      background-color: #d4edda;
      border-color: #c3e6cb;
      color: #155724;
    }
    
    .alert-error {
      background-color: #f8d7da;
      border-color: #f5c6cb;
      color: #721c24;
    }
    
    .alert-warning {
      background-color: #fff3cd;
      border-color: #ffeaa7;
      color: #856404;
    }
    
    .alert-info {
      background-color: #d1ecf1;
      border-color: #bee5eb;
      color: #0c5460;
    }
  `]
})
export class AlertComponent {
  @Input() type: AlertType = 'info';
  @Input() message: string = '';
  @Input() show: boolean = true;
  @Input() dismissible: boolean = false;
  @Output() close = new EventEmitter<void>();

  get alertClass(): string {
    return `alert-${this.type}`;
  }

  getIcon(): string {
    const icons = {
      success: '✓',
      error: '✗',
      warning: '⚠',
      info: 'ℹ'
    };
    return icons[this.type] || 'ℹ';
  }

  onClose(): void {
    this.close.emit();
  }
} 