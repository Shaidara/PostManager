import { Injectable } from '@angular/core';
import { Post } from '../../types/post';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  baseUrl = 'https://localhost:7074/api';

  async getPosts(
    tags: string,
    sortBy: string,
    direction: string
  ): Promise<Post[]> {
    try {
      const endpoints = `posts?tags=${tags}&sortBy=${sortBy}&direction=${direction}`;
      const data = await fetch(encodeURI(`${this.baseUrl}/${endpoints}`));
      return (await data.json()).data ?? [];
    } catch (error) {
      console.log('An error occured while fetching posts', error);
      return [];
    }
  }
}
