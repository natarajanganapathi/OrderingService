import { Routes } from '@angular/router';
import { DashboardComponent } from 'app/dashboard/dashboard.component';
import { OrdersComponent } from 'app/orders/orders.component';
import { CreateOrderComponent } from 'app/create-order/create-order.component';
import { AccountComponent } from 'app/account/account.component';
import { CreateAccountComponent } from 'app/create-account/create-account.component';
import { CatalogComponent } from 'app/catalog/catalog.component';
import { CreateCatalogComponent } from 'app/create-catalog/create-catalog.component';

export const AdminLayoutRoutes: Routes = [
    {
      path: '',
      children: [ {
        path: 'dashboard',
        component: DashboardComponent
    }]}, //{
    // path: '',
    // children: [ {
    //   path: 'userprofile',
    //   component: UserProfileComponent
    // }]
    // }, {
    //   path: '',
    //   children: [ {
    //     path: 'icons',
    //     component: IconsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'notifications',
    //         component: NotificationsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'maps',
    //         component: MapsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'typography',
    //         component: TypographyComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'upgrade',
    //         component: UpgradeComponent
    //     }]
    // },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'create-order/:mode/:id', component: CreateOrderComponent },
    { path: 'orders', component: OrdersComponent },
    { path: 'account', component: AccountComponent },
    { path: 'create-account/:mode/:id', component: CreateAccountComponent },
    { path: 'catalog', component: CatalogComponent },
    { path: 'create-catalog/:mode/:id', component: CreateCatalogComponent },
];
