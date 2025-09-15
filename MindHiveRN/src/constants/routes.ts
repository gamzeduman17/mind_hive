// Route names (constants)
export const ROUTES = {
  LOGIN: 'Login',
  HOME: 'Home',
  PROFILE: 'Profile',
  SETTINGS: 'Settings',
} as const;

// Type for stack navigation
export type RootStackParamList = {
  [ROUTES.LOGIN]: undefined;
  [ROUTES.HOME]: undefined;
  [ROUTES.PROFILE]: undefined;
  [ROUTES.SETTINGS]: undefined;
};
