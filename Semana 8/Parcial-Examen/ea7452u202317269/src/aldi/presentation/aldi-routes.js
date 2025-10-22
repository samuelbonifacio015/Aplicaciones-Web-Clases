
const newIssues = () => import('./views/new-issue.vue');

const aldiRoutes = [
    { path: 'issues', name:'new-issue', component: newIssues, meta: { title: 'New Issue' } },
]

export default aldiRoutes;