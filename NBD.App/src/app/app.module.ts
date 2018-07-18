import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { GoalFormComponent } from './goals/goal-form/goal-form.component';
import { GoalTreeComponent } from './goals/goal-tree/goal-tree.component';
import { GoalTreeNodeComponent } from './goals/goal-tree-node/goal-tree-node.component';
import { GoalsService } from './goals/goals.service';

const appRoutes: Routes = [
  { path: 'tree', component: GoalFormComponent },
  { path: 'trees/:id', component: GoalTreeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    GoalFormComponent,
    GoalTreeComponent,
    GoalTreeNodeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [GoalsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
