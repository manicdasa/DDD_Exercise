import 'bootstrap/dist/css/bootstrap.css';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import * as Sentry from "@sentry/react";
import { Integrations } from "@sentry/tracing";
import store from './store/store'
import { App } from './App';
import registerServiceWorker from './registerServiceWorker';
import { transitions, positions, Provider as AlertProvider } from 'react-alert';
import { AlertTemplate } from '../src/components/Layout/AlertTemplate';
import ErrorBoundary from './components/ErrorBoundary';


Sentry.init({
    dsn: "https://8854c24efb4b46fdb4792e88ab6b3ec4@o908955.ingest.sentry.io/5844469",
    integrations: [new Integrations.BrowserTracing()],

    // Set tracesSampleRate to 1.0 to capture 100%
    // of transactions for performance monitoring.
    // We recommend adjusting this value in production
    tracesSampleRate: 1.0,
});

const options = 
{
    position: positions.TOP_CENTER,
    timeout: 5000,
    offset: '30px',
    transition: transitions.FADE
}

ReactDOM.render(
    <ErrorBoundary>
  <AlertProvider template={AlertTemplate} {...options}>
        <Provider store={store}>
            
                <App />
                
    </Provider>
  </AlertProvider>
        </ErrorBoundary>,
  document.getElementById('root'));
registerServiceWorker();
