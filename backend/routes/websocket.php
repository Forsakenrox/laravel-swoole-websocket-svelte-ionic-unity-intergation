<?php


use Illuminate\Http\Request;
use Illuminate\Support\Facades\Log;
use SwooleTW\Http\Server\Facades\Server;
use SwooleTW\Http\Websocket\Facades\Room;
use SwooleTW\Http\Websocket\Facades\Websocket;
use SwooleTW\Http\Websocket\Rooms\RedisRoom;

/*
|--------------------------------------------------------------------------
| Websocket Routes
|--------------------------------------------------------------------------
|
| Here is where you can register websocket events for your application.
|
*/

Websocket::on('connect', function ($websocket, Request $request) {
    // Room::add(Websocket::getSender(), 'backet-room');
    // Log::info(31);
    // Server::tick(1000, function () use ($websocket) {
    //     $websocket->emit('message', 'ping');
    // });
    // swoole_timer_tick(10000, function ($timer_id) use ($websocket) {
    //     $websocket->emit('message', 'ping');
    // });
    // $websocket->join('test_room');
    // Room::addValue(1, "kek");
    $websocket->emit('message', 'welcome, you connected');
});

Websocket::on('disconnect', function ($websocket) {
    // called while socket on disconnect
});

Websocket::on('submit-controller', function ($websocket, Request $request) {
    $websocket->emit('message', "your fd is: " . Websocket::getSender() . ", you are welcome");
    if (!in_array(Websocket::getSender(), Room::getClients('controllers'))) {
        Room::add(Websocket::getSender(), 'controllers');
        $websocket->emit('status', ['online' => true]);
    } else {
        Room::delete(Websocket::getSender(), 'controllers');
        $websocket->emit('status', ['online' => false]);
    }
});

Websocket::on('test', function ($websocket, $data) {
    Log::info($data);
    $websocket->emit('message', $data);
});

Websocket::on('orientation', function ($websocket, $data) {
    $websocket->broadcast()->emit('message', $data);
    $websocket->emit('message', $data);
});
