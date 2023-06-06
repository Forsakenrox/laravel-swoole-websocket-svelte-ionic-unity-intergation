<?php

use Illuminate\Support\Facades\Log;
use Illuminate\Support\Facades\Route;
use SwooleTW\Http\Table\Facades\SwooleTable;
use SwooleTW\Http\Table\SwooleTable as TableSwooleTable;
use SwooleTW\Http\Websocket\Facades\Room;

/* 
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/

Route::get('/tables', function () {
    $tables = SwooleTable::getAll();
    // SwooleTable::add('lol', new Swoole\Table(1000));
    $fds = Room::getClients('lolkaroom');
    // Log::info(SwooleTable::get('lol'));
    // Log::info(SwooleTable::getAll());

    return response('dywdwa' . json_encode($fds));
});
