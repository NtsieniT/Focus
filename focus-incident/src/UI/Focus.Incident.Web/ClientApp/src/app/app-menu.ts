import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'Home',
    icon: 'nb-home',
    link: '/home',
    home: true
  },
  {
    title: 'Features',
    group: true
  },
  {
    title: 'Business Lines',
    icon: 'nb-location',
    link: '/businessLine', 
  },
  {
    title: 'Applications',
    icon: 'nb-gear',
    link: '/application',
  }
  ,
  {
    title: 'Assignment Groups',
    icon: 'nb-compose',
    link: '/applicationGroup',
  },
  {
    title: 'People',
    icon: 'nb-person',
    link: '/person',
  },
  {
    title: 'Summary',
    icon: 'nb-star',
    link: '/SummaryOverview',
  },
  {
    title: 'Overview',
    icon: 'nb-bar-chart',
    link: '/Overview',
  }
];
