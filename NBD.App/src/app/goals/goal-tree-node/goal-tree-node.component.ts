import { Component, Input } from '@angular/core';

import { Goal } from '../goal';
 
@Component({
  selector: 'goal-tree-node',
  templateUrl: './goal-tree-node.component.html',
  styleUrls: ['./goal-tree-node.component.css']
})
export class GoalTreeNodeComponent {
  @Input() node: Goal;
}