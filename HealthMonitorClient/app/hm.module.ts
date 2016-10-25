import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { WSConnectionComponent }   from './ws.connection/ws.connection.component';

@NgModule({
    imports: [BrowserModule],
    declarations: [WSConnectionComponent],
    bootstrap: [WSConnectionComponent]
})
export class HMModule { }