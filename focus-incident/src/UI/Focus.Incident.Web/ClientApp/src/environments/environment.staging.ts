export const environment = {
  production: false,
  serviceUrl: 'https://retinadev.intra.absa.co.za:8030',
  pageSize: 20,
  pageSizes: [20, 100, 500, 1000],
  portalUrl: 'https://retinadev.intra.absa.co.za:8051',
  authentication: {
    authority: 'https://retinadev.intra.absa.co.za:8000',
    client_id: 'Focus.Incident.Web',
    redirect_uri: 'https://retinadev.intra.absa.co.za:8031/signin-callback.html',
    post_logout_redirect_uri: 'https://retinadev.intra.absa.co.za:8031/',
    silent_redirect_uri: 'https://retinadev.intra.absa.co.za:8031/silent-renew.html',
    scope: 'openid profile Focus.Incident.API Focus.Identity.Authorization'
  },
  authorization: {
    authority: "https://retinadev.intra.absa.co.za:8001"
  }
};
