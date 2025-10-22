import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/shared/presentation/views/home.vue';
import NewIssue from '@/aldi/presentation/views/new-issue.vue';
import NextServiceOrdersList from '@/aldi/presentation/views/next-service-orders-list.vue';
import IssueAnalytics from '@/aldi/presentation/views/issue-analytics.vue';
import PageNotFound from '@/shared/presentation/views/page-not-found.vue';

const routes = [
    {
    path: '/',
    redirect: '/home'
    },
  {
    path: '/home',
    name: 'Home',
    component: Home
  },
  {
    path: '/new-issue',
    name: 'NewIssue',
    component: NewIssue
  },
  {
    path: '/service-orders',
    name: 'ServiceOrders',
    component: NextServiceOrdersList
  },
  {
    path: '/analytics',
    name: 'Analytics',
    component: IssueAnalytics
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'PageNotFound',
    component: PageNotFound
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
    console.log(`Navigating from ${from.name} to ${to.name}`);
    let baseTitle = 'Aldi Operations Continuity Platform';
    document.title = `${baseTitle} - ${to.meta['title']}`;
    next();
});

export default router;
