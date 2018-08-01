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
  private showAddForm: boolean;
  private showProgressInputField: boolean;
  private addFormToggleText: string;
  private makeProgressText: string;
  private progress: number;
  private progressFieldValue: number;

  constructor(private goalsService: GoalsService, private sanitizer: DomSanitizer) {
    this.addFormToggleText = 'Add';
  }

  ngOnInit() {
    this.makeProgressText = this.node.target == null ? 'Finish' : 'Make progress';
    this.progress = (this.node.progress / (this.node.target === null ? 1 : this.node.target)) * 100;
    this.showProgressInputField = (this.node.target != null);
  }

  onAddClick() {
    this.showAddForm = !this.showAddForm;
    this.addFormToggleText = this.showAddForm ? 'Close' : 'Add';
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