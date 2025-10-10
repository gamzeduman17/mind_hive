import { Button, StyleSheet, Text, View } from "react-native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { useNavigation } from "@react-navigation/native";
import { RootStackParamList, ROUTES } from "../../constants/routes";

type HomeScreenNavigationProp = NativeStackNavigationProp<RootStackParamList,
    typeof ROUTES.HOME>;
export default function HomeScreen() {
    const navigation = useNavigation<HomeScreenNavigationProp>();
    return (
        <View style={styles.container}>
            <Text style={styles.text}>
                Welcome to Home Screen
            </Text>
            <Button title="Go to Profile"
                onPress={() => navigation.navigate(ROUTES.PROFILE)} />
            <Button title="Settings" onPress={() => navigation.navigate(ROUTES.SETTINGS)} />
            <View style={{ marginTop: 20 }}>
                <Button title="Logout" color="red" onPress={() => navigation.replace(ROUTES.LOGIN)} />
                {/* chat böyle yapmış dene !!!! onPress={() => navigation.replace("Login")} */}
            </View>
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center',
    },
    text: {
        fontSize: 24,
        fontWeight: '600',
    }
})