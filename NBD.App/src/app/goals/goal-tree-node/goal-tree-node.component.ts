import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer  } from '@angular/platform-browser';

import { Goal } from '../goal';
import { GoalsService } from '../goals.service';
 
@Component({
  selector: 'goal-tree-node',
  templateUrl: './goal-tree-node.component.html',
  styleUrls: ['./goal-tree-node.component.css']
})
export class GoalTreeNodeComponent implements OnInit {
  @Input() node: Goal;
  private showForm: boolean;
  private showProgressInputField: boolean;
  private addFormToggleText: string;
  private editFormToggleText: string;
  private makeProgressText: string;
  private progress: number;
  private progressFieldValue: number;
  private lastClickedEdit: boolean;

  constructor(private goalsService: GoalsService, private sanitizer: DomSanitizer) {
    this.addFormToggleText = 'Add';
    this.editFormToggleText = 'Edit';
  }

  ngOnInit() {
    if (this.node == null) {
      return;
    }
    
    this.makeProgressText = this.node.target == null ? 'Finish' : 'Make progress';
    this.progress = (this.node.progress / (this.node.target === null ? 1 : this.node.target)) * 100;
    this.showProgressInputField = (this.node.target != null);
  }

  onAddClick() {
    this.showForm = !this.showForm;
    this.addFormToggleText = this.showForm ? 'Close' : 'Add';
    this.lastClickedEdit = false;
  }

  onEditClick() {
    this.showForm = !this.showForm;
    this.editFormToggleText = this.showForm ? 'Close' : 'Edit';
    this.lastClickedEdit = true;
  }

  onDeleteClick() {
    this.goalsService.deleteGoal(this.node.id).subscribe();
  }

  onMakeProgressClick() {
    this.goalsService.makeProgress(this.node.id, this.progressFieldValue).subscribe();
  }

  getProgressBar() {
    return this.sanitizer.bypassSecurityTrustStyle(
      'linear-gradient(to right, black ${this.progress}%, white {100 - this.progress}%);');
  }
}