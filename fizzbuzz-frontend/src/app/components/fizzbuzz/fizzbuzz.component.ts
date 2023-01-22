import {Component} from '@angular/core';
import {FizzBuzzService} from '../../services/fizzbuzz.service';

@Component({
  selector: 'app-fizz-buzz',
  templateUrl: './fizzbuzz.component.html',
  styleUrls: ['./fizzbuzz.component.sass']
})
export class FizzBuzzComponent {

  inputNumber: number = 0;
  maxNumber: number = 20;
  results: any = [];

  constructor(private service: FizzBuzzService) {
  }

  onKey(event: any) {
    this.maxNumber = parseInt(event.target.value);
  }

  onShuffle() {
    this.inputNumber = parseInt(((Math.random() * (this.maxNumber - 1))).toFixed(0));
    this.doProcess();
  }

  doProcess() {
    this.service.processResultsOnApi(this.inputNumber, this.maxNumber).subscribe(res => {
      this.results = [];
      const elements = res.value.split(",");
      this.results = elements.filter((item: string, index: number) => index != 0);
    });
  }
}
