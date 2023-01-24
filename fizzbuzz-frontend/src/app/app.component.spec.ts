import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientModule } from '@angular/common/http';
import { FizzBuzzComponent } from './components/fizzbuzz/fizzbuzz.component';
import { HeaderComponent } from './components/header/header.component';
import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { ButtonComponent } from './components/button/button/button.component';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientModule, MatSnackBarModule
      ],
      declarations: [
        AppComponent, HeaderComponent, FizzBuzzComponent, ButtonComponent
      ],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'fizzbuzz-frontend'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('fizzbuzz-frontend');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('header>h1')?.textContent).toContain('FizzBuzz App');
  });
});
