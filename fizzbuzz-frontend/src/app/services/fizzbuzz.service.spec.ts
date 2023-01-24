import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

import { FizzBuzzService } from './fizzbuzz.service';

describe('FizzBuzzService', () => {
  let service: FizzBuzzService;

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [HttpClient, HttpHandler, MatSnackBar] });
    service = TestBed.inject(FizzBuzzService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
