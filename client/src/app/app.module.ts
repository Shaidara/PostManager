import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { PostComponent } from './post/post.component';
import { ItemComponent } from './item/item.component';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [AppComponent, PostComponent, ItemComponent],
  imports: [BrowserModule, MatIconModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
