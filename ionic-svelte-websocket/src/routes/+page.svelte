<script>
    // import { PluginListenerHandle } from "@capacitor/core";
    import { Motion } from "@capacitor/motion";
    import io from "socket.io-client";
    const socket = io("ws://192.168.88.156", {
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
    let current_xyz2 = {
        x: 0,
        y: 0,
        z: 0,
    };
    let current_xyz3 = {
        x: 0,
        y: 0,
        z: 0,
    };
    let current_xyz4 = {
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
    let accelHandler2;

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
        accelHandler = await Motion.addListener("accel", (event) => {
            console.log(event);
            current_xyz.x = event?.acceleration.x?.toFixed(2);
            current_xyz.y = event?.acceleration.y?.toFixed(2);
            current_xyz.z = event?.acceleration.z?.toFixed(2);
            current_xyz2.x = event?.accelerationIncludingGravity.x?.toFixed(2);
            current_xyz2.y = event?.accelerationIncludingGravity.y?.toFixed(2);
            current_xyz2.z = event?.accelerationIncludingGravity.z?.toFixed(2);
            current_xyz3.x = event?.rotationRate?.alpha?.toFixed(2);
            current_xyz3.y = event?.rotationRate?.beta?.toFixed(2);
            current_xyz3.z = event?.rotationRate?.gamma?.toFixed(2);
        });
        accelHandler2 = await Motion.addListener("orientation", (event) => {
            console.log(event);
            current_xyz4.x = event?.alpha?.toFixed(2);
            current_xyz4.y = event?.beta?.toFixed(2);
            current_xyz4.z = event?.gamma?.toFixed(2);
        });
        // accelHandler2 = await Motion.addListener("accel", (event) => {
        //     current_xyz2.x = event?.alpha?.toFixed(2);
        //     current_xyz2.y = event?.beta?.toFixed(2);
        //     current_xyz2.z = event?.gamma?.toFixed(2);
        // });
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
    <p>acceleration</p>
    <ion-item>
        <ion-label>x: {current_xyz.x} y:{current_xyz.y} z:{current_xyz.z}</ion-label>
    </ion-item>
    <p>acceleration include gravity</p>
    <ion-item>
        <ion-label>x: {current_xyz2.x} y:{current_xyz2.y} z:{current_xyz2.z}</ion-label>
    </ion-item>
    <p>rotation rate (accel)</p>
    <ion-item>
        <ion-label>x: {current_xyz3.x} y:{current_xyz3.y} z:{current_xyz3.z}</ion-label>
    </ion-item>
    <p>orientation (orientation)</p>
    <ion-item>
        <ion-label>x: {current_xyz4.x} y:{current_xyz4.y} z:{current_xyz4.z}</ion-label>
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
