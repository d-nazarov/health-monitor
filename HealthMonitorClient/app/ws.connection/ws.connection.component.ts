import { Component } from '@angular/core';

declare var alertify: any;

@Component({
    selector: 'ws-connection',
    templateUrl: 'app/ws.connection/ws.connection.template.html'
})
export class WSConnectionComponent {
    public processes = null;
    public isConnected = false;
    public ws = null;

    connect() {
        this.ws = new WebSocket("ws://127.0.0.1:8181");

        this.ws.onopen = function () { this.isConnected = true; }.bind(this);

        this.ws.onclose = function () { this.isConnected = false; }.bind(this);

        this.ws.onerror = function (error) { alertify.error('Error!'); };

        this.ws.onmessage = function (evt) {
            var message = JSON.parse(evt.data);

            if (message.messageType == 'ProcessesInformation') {
                this.processes = message.data;
            }
            else {
                alertify.warning(message.message + '(' + message.data + ')')
            }
        }.bind(this);
    }

    disconnect() {
        this.ws.close();
        this.processes = null;
    }

}