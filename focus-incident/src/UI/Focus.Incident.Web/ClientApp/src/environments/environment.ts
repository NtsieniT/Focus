// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --configuration=Production` then `environment.Production.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular.json`.

export const environment = {
  production: false,
  serviceUrl: 'https://localhost:8030',
  pageSize: 20,
  pageSizes: [20, 100, 500, 1000],
  portalUrl: 'https://localhost:8051',
  authentication: {
    authority: 'https://retinadev.intra.absa.co.za:8000', 
    client_id: 'Focus.Incident.Web',
    redirect_uri: 'https://localhost:8031/signin-callback.html',
    post_logout_redirect_uri: 'https://localhost:8031/',
    silent_redirect_uri: 'https://localhost:8031/silent-renew.html',
    scope: 'openid profile Focus.Incident.API Focus.Identity.Authorization'
  },
  authorization: {
    authority: "https://retinadev.intra.absa.co.za:8001" 
  }
};
