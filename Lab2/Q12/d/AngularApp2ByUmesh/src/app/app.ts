import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  currentView: 'home' | 'calculator' = 'home';
  currentYear: number = new Date().getFullYear();

  // Calculator properties
  num1: number | null = null;
  num2: number | null = null;
  operation: string = 'add';
  result: number | null = null;

  setView(view: 'home' | 'calculator') {
    this.currentView = view;
  }

  compute() {
    if (this.num1 === null || this.num2 === null) {
      this.result = null;
      return;
    }

    const n1 = Number(this.num1);
    const n2 = Number(this.num2);

    switch (this.operation) {
      case 'add':
        this.result = n1 + n2;
        break;
      case 'subtract':
        this.result = n1 - n2;
        break;
      case 'multiply':
        this.result = n1 * n2;
        break;
    }
  }
}