import { configureStore } from '@reduxjs/toolkit';
import landingPageReducer from './LandingPageReducer';
import searchAuthorReducer from './SearchAuthorReducer';
import signInReducer from './SignInReducer'
import authorInformationsReducer from './AuthorInformationsReducer';
import authorOffersReducer from './AuthorOffersReducer';
import authorActiveProjectsReducer from './AuthorActiveProjectsReducer';
import customerReducer from './CustomerReducer';
import createProjectReducer from './CreateProjectReducer';
import requestBookingReducer from './RequestBookingReducer';
import adminDashboardReducer from './AdminDashboard';
import connectionReducer from './ConnectionReducer';
import notificationsReducer from './NotificationReducer'
import assignProjectReducer from './AssignProjectReducer'
import sidePanelReducer from './SidePanelReducer';
import chatComponentsReducer from './ChatComponentReducer';

export const store = configureStore({
    reducer: {
      landingPageReducer: landingPageReducer,
      searchAuthorReducer: searchAuthorReducer,
      signInReducer: signInReducer,
      authorInformationsReducer: authorInformationsReducer,
      authorOffersReducer: authorOffersReducer,
      authorActiveProjectsReducer: authorActiveProjectsReducer,
      customerReducer: customerReducer,
      createProjectReducer: createProjectReducer,
      requestBookingReducer: requestBookingReducer,
      adminDashboardReducer: adminDashboardReducer,
      connectionReducer: connectionReducer,
      notificationsReducer: notificationsReducer,
      assignProjectReducer: assignProjectReducer,
      sidePanelReducer: sidePanelReducer,
      chatComponentsReducer: chatComponentsReducer
    },
});

export type RootState = ReturnType<typeof store.getState>

export default store;