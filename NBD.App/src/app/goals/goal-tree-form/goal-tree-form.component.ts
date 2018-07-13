import { Component } from '@angular/core';

import { GoalsService } from '../goals.service';
import { GoalTree } from '../goal-tree';
import { Goal } from '../goal';

@Component({
    selector: 'goal-tree-form',
    templateUrl: './goal-tree-form.component.html',
    styleUrls: ['./goal-tree-form.component.css']
})
export class GoalTreeFormComponent {
    goalTree: GoalTree;

    constructor(private goalsService: GoalsService) { 
        this.goalTree = new GoalTree();
        this.goalTree.rootGoal = new Goal();
    }

    onSubmit() {
        this.goalsService.createTree(this.goalTree);
    }
}