import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { ShopService } from '.';

@Injectable()
export class ShopListResolver implements Resolve<any> {
  constructor(private shopService: ShopService) {}

  resolve() {
    const shops = this.shopService.getShops();
    return shops;
  }
}
