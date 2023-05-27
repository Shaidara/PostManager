import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should set default tags, sortBy and direction`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.allItems.length).toEqual(1);
    expect(app.allItems[0]).toEqual('tech');
    expect(app.sortBy).toEqual('id');
    expect(app.direction).toEqual('asc');
  });

  it('should render page welcome message', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1.welcome')?.textContent).toContain(
      'Welcome to this posts displayer'
    );
  });
});
