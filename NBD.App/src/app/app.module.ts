import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { GoalFormComponent } from './goals/goal-form/goal-form.component';
import { GoalTreeComponent } from './goals/goal-tree/goal-tree.component';
import { GoalTreeNodeComponent } from './goals/goal-tree-node/goal-tree-node.component';
import { GoalsService } from './goals/goals.service';
import { RecurrencePipe } from './pipes/recurrence.pipe';

const appRoutes: Routes = [
  { path: 'tree', component: GoalFormComponent },
  { path: 'trees/:id', component: GoalTreeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    GoalFormComponent,
    GoalTreeComponent,
    GoalTreeNodeComponent,
    RecurrencePipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    { provide: 'BASE_TRACKER_API_URL', useValue: 'http://localhost:50401/api' },
    HttpClient, 
    GoalsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
