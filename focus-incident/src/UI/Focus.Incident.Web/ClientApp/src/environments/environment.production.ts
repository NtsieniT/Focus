export const environment = {
  production: false,
  serviceUrl: 'https://focus11.absa.co.za:8030',
  pageSize: 20,
  pageSizes: [20, 100, 500, 1000],
  portalUrl: 'https://focus12.absa.co.za:8051',
  authentication: {
    authority: 'https://focus11.absa.co.za:8000',
    client_id: 'Focus.Incident.Web',
    redirect_uri: 'https://focus12.absa.co.za:8031/signin-callback.html',
    post_logout_redirect_uri: 'https://focus12.absa.co.za:8031/',
    silent_redirect_uri: 'https://focus12.absa.co.za:8031/silent-renew.html',
    scope: 'openid profile Focus.Incident.API Focus.Identity.Authorization'
  },
  authorization: {
    authority: "https://focus11.absa.co.za:8001"
  }
};
