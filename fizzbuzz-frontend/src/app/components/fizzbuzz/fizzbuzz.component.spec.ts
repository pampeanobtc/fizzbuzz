import { ButtonComponent } from './../button/button/button.component';
import { HeaderComponent } from './../header/header.component';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FizzBuzzComponent } from './fizzbuzz.component';
import { FizzBuzzService } from '../../services/fizzbuzz.service';

describe('FizzBuzzComponent', () => {
  let component: FizzBuzzComponent;
  let fixture: ComponentFixture<FizzBuzzComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FizzBuzzComponent, HeaderComponent, ButtonComponent ],
      providers: [ FizzBuzzService, HttpClient, HttpHandler ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FizzBuzzComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create fizz-buzz', () => {
    expect(component).toBeTruthy();
  });

  it(`should result be an fizz-buzz array`,() => {
    expect(Array.isArray(component.doProcess)).toBeFalsy();
  })
});
