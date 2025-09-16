import { NavigationContainer } from "@react-navigation/native";
import HomeScreen from "../screens/HomeScreen";
import LoginScreen from "../screens/LoginScreen";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { RootStackParamList, ROUTES } from "../constants/routes";
import ProfileScreen from "../screens/ProfileScreen";
import { enableScreens } from 'react-native-screens';

enableScreens();

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function AppNavigator() {
    return (
        <NavigationContainer>
            <Stack.Navigator initialRouteName={ROUTES.LOGIN} screenOptions={{ headerShown: true }}>
                <Stack.Screen name={ROUTES.HOME} component={HomeScreen} options={{ title: "MindHive" }}></Stack.Screen>
                <Stack.Screen name={ROUTES.LOGIN} component={LoginScreen} options={{ title: "Login" }}></Stack.Screen>
                <Stack.Screen name={ROUTES.PROFILE} component={ProfileScreen} options={{ title: "Profile" }}></Stack.Screen>
            </Stack.Navigator>
        </NavigationContainer>
    )
}