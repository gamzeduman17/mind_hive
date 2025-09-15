import { Button, StyleSheet, Text, View } from "react-native";

export default function LoginScreen({ navigation }: any) {
    return (
        <View style={styles.container}>
            <Text style={styles.text}>
                Login Screen
            </Text>
            <Button title="Go to Home" onPress={() => navigation.navigate('Home')}>

            </Button >
        </View>
    )
}
const styles=StyleSheet.create({
    container:{
        flex:1,
        alignItems:'center',
        justifyContent:'center'
    },
    text:{
        fontSize:24,
        fontWeight:'600',
        marginBottom:20
    }
})