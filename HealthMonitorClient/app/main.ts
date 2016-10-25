import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { HMModule } from './hm.module';

const platform = platformBrowserDynamic();
platform.bootstrapModule(HMModule);