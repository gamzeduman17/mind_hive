import { NavigationContainer } from "@react-navigation/native";

import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { RootStackParamList, ROUTES } from "../constants/routes";
import ProfileScreen from "../screens/Profile/ProfileScreen";
import { enableScreens } from 'react-native-screens';
import SettingsScreen from "../screens/Settings/SettingScreen";
import HomeScreen from "../screens/Home/HomeScreen";
import LoginScreen from "../screens/Auth/LoginScreen";

enableScreens();

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function AppNavigator() {
    return (
        <NavigationContainer>
            <Stack.Navigator initialRouteName={ROUTES.LOGIN} screenOptions={{ headerShown: true }}>
                <Stack.Screen name={ROUTES.HOME} component={HomeScreen} options={{ title: "MindHive" }}></Stack.Screen>
                <Stack.Screen name={ROUTES.LOGIN} component={LoginScreen} options={{ title: "Login" }}></Stack.Screen>
                <Stack.Screen name={ROUTES.PROFILE} component={ProfileScreen} options={{ title: "Profile" }}></Stack.Screen>
                <Stack.Screen name={ROUTES.SETTINGS} component={SettingsScreen} options={{ title: "Settings" }}></Stack.Screen>
            </Stack.Navigator>
        </NavigationContainer>
    )
}