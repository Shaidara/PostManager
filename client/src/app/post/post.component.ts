import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Post } from 'src/types/post';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css'],
})
export class PostComponent implements OnInit {
  @Input() post!: Post;

  Math = Math;
  constructor() {}

  ngOnInit(): void {}
}
