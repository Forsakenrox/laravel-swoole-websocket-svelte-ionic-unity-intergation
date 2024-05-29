# laravel-swoole-websocket-svelte-ionic-unity-intergation
example of worked integration of swoole websockets with php laravel-swoole, ionic app writen with svelte and Unity game engine

![ezgif-2-a0c65fb5b7](https://github.com/Forsakenrox/laravel-swoole-websocket-svelte-ionic-unity-intergation/assets/11795286/2c9e40e8-f6be-4d89-8c3a-2e5900623dae)
Если коротко, то в ионике используется клиент socket.io на js который на момент создания проекта имел 4 разные версии протокола, к сожалению та что реализована в проекте laravel-swoole - версия 2, то есть ионик пришлось настраивать именно на работу
с этим протоколом, плюс кое чё в самом swoole пришлось допилить. В юнити для соединения с этим кривым сервером используется SocketIOClient.Newtonsoft, который к сожалению соединяется через раз. вернее в 1 раз из 100, но тем не менее - мне удалось запустить вебсокет сервер на php и передавать данные с гироскопа
телефона 60 фреймов в секунду без каких либо проблем ( при загрузке виртуальной машины на 3-4%). То есть такие проекты не теория, а реальность.

установка:

npm i
npm run build
npx cap add android
npx cap sync
npx cap open android

composer i

php artisan:http start или как то так в папке со свулей

ионик можно скомпилировать через капаситор и пульнуть в эмулятор в android studio и от туда уже играться.
