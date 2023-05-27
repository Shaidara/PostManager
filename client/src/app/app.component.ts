import { Component } from '@angular/core';
import { Post } from 'src/types/post';
import { PostService } from './services/post.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  sortBy = 'id';
  direction = 'asc';

  allItems: string[] = ['tech'];
  sortProperties: string[] = ['id', 'reads', 'likes', 'popularity'];

  directionProperties: string[] = ['asc', 'desc'];

  posts: Post[] = [];
  postService: PostService = new PostService();

  constructor() {
    this.send();
  }

  remove(tag: string) {
    this.allItems.splice(this.allItems.indexOf(tag), 1);
  }

  addTag(tag: string) {
    const formatted = tag.trim().replace(',', '');
    if (!formatted) return;
    if (this.allItems.indexOf(formatted) == -1) {
      this.allItems.unshift(formatted);
    }
  }

  onSortingDirectionChange(e: Event) {
    const val = (e.target as HTMLInputElement).value;
    this.direction = val;
  }

  onSortingPropertyChange(e: Event) {
    const val = (e.target as HTMLInputElement).value;
    this.sortBy = val;
  }

  async send() {
    if (this.allItems.length <= 0) return;

    let tags = `${this.allItems[0]},`;
    for (let index = 0; index < this.allItems.length; index++) {
      const element = this.allItems[index];
      tags += `${element},`;
    }

    this.posts = await this.postService.getPosts(
      tags,
      this.sortBy,
      this.direction
    );
  }
}
