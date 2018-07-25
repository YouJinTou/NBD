import { Component, Input } from '@angular/core';
import { DomSanitizer  } from '@angular/platform-browser';

import { Goal } from '../goal';
 
@Component({
  selector: 'goal-tree-node',
  templateUrl: './goal-tree-node.component.html',
  styleUrls: ['./goal-tree-node.component.css']
})
export class GoalTreeNodeComponent {
  @Input() node: Goal;
  private showAddForm: boolean;
  private addFormToggleText: string;
  private progress: number;

  constructor(private sanitizer: DomSanitizer) {
    this.addFormToggleText = 'Add';
    this.progress = 0; //(this.node.progress / (this.node.target === null ? 1 : this.node.target)) * 100;
  }

  onAddClick() {
    this.showAddForm = !this.showAddForm;
    this.addFormToggleText = this.showAddForm ? 'Close' : 'Add';
  }

  getProgressBar() {
    return this.sanitizer.bypassSecurityTrustStyle(
      'linear-gradient(to right, black ${this.progress}%, white {100 - this.progress}%);');
  }
}