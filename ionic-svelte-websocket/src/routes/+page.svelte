<script lang="ts">
    // import { PluginListenerHandle } from "@capacitor/core";
    import { Motion } from "@capacitor/motion";
    import io from "socket.io-client";
    const socket = io("ws://192.168.46.130", {
        withCredentials: false,
        // transports: ["websocket"], //if you want force wss connection instead of initial http
    });
    let messages = [];
    let isConnected = false;
    let buttonText = "Connect";
    let current_xyz = {
        x: 0,
        y: 0,
        z: 0,
    };
    let recieved_xyz = {
        x: 0,
        y: 0,
        z: 0,
    };
    let accelevent;

    socket.on("connect", function (event) {
        console.log(event);
    });
    socket.on("status", function (event) {
        console.log(event.online);
        isConnected = event.online;
    });
    socket.on("message", function (event) {
        console.log(event);
    });

    $: if (isConnected == true) {
        buttonText = "Disconnect";
    } else {
        buttonText = "Connect";
    }

    function handleClick() {
        socket.emit("submit-controller", { text: "i want to be controller" });
    }
    function handleClick2() {
        socket.emit("test", { text: "i want to be controller" });
    }

    let accelHandler;

    function sendOrientation() {
        socket.emit("orientation", current_xyz);
        console.log("orientation sended");
    }
    async function requestPermission() {
        // try {
        //     await DeviceMotionEvent.requestPermission();
        // } catch (e) {
        //     // Handle error
        //     return;
        // }

        // Once the user approves, can start listening:
        accelHandler = await Motion.addListener("orientation", (event) => {
            current_xyz.x = event?.alpha?.toFixed(2);
            current_xyz.y = event?.beta?.toFixed(2);
            current_xyz.z = event?.gamma?.toFixed(2);
            // console.log("Device motion event:", event);
        });
        setInterval(sendOrientation, 32);
    }

    // Stop the acceleration listener
    const stopAcceleration = () => {
        if (accelHandler) {
            accelHandler.remove();
        }
    };

    // Remove all listeners
    const removeListeners = () => {
        Motion.removeAllListeners();
    };
</script>

<ion-card>
    <ion-header style="{isConnected ? 'color:green' : 'color:brown'}">{isConnected}</ion-header>
    <ion-button on:click="{handleClick}" on:keydown="{handleClick}">{buttonText}</ion-button>
    <ion-button on:click="{handleClick2}" on:keydown="{handleClick2}">test</ion-button>
    <ion-button on:click="{requestPermission}" on:keydown="{requestPermission}">request permission</ion-button>
    <ion-item>
        <ion-label>x: {current_xyz.x} y:{current_xyz.y} z:{current_xyz.z}</ion-label>
    </ion-item>
    <ion-item>
        <ion-label>{accelevent?.acceleration?.x} {accelevent?.acceleration?.y} {accelevent?.acceleration.z}</ion-label>
    </ion-item>
    <ion-item>
        <ion-label>{accelevent?.accelerationIncludingGravity?.x} {accelevent?.accelerationIncludingGravity.y} {accelevent?.accelerationIncludingGravity.z}</ion-label>
    </ion-item>
    <ion-item>
        <ion-label>{accelevent?.rotationRate.alpha} {accelevent?.rotationRate.beta} {accelevent?.rotationRate.gamma}</ion-label>
    </ion-item>
    {#each messages as message}
        <ion-item>
            <ion-label>{message}</ion-label>
            <ion-button href="https://ionicsvelte.firebaseapp.com/" target="_new" fill="outline" slot="end">View</ion-button>
        </ion-item>
    {/each}
</ion-card>
