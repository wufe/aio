const spawn = require('cross-spawn-with-kill');
const cwd = process.cwd();

// Borrowed from https://github.com/dotnet/aspnetcore/blob/1bf292d47ac2a0ebda07d8a3f00355dd01915ad5/src/Middleware/NodeServices/src/TypeScript/Util/ExitWhenParentExits.ts
const processExists = pid => {
    try {
        process.kill(+pid, 0);
        return true;
    } catch (ex) {
        if (ex.code === 'EPERM') {
            throw new Error(`Attempted to check whether process ${pid} was running, but got a permissions error.`);
        }

        return false;
    }
};


(async () => {

    if (process.argv.length <= 3) {
        console.log('Usage: node phandler.js <parent-pid> <app> [<arg>, ..]')
        process.exit(1);
    }

    const parentPID = process.argv[2];
    const app = process.argv[3];
    const args = process.argv.slice(4);

    const proc = spawn(app, args, { cwd });
    proc.stdout.on('data', (data) => {
        console.log(data.toString());
    });
    proc.stderr.on('data', (data) => {
        console.error(data.toString());
    });

    setInterval(async () => {
        if (!processExists(parentPID)) {
            proc.kill();
            setTimeout(() => process.exit(), 5000);
        } else {
        }
    }, 10000);

    process.on('SIGNINT', () => (`Received SIGINT. Waiting for parent process to exit.`));

})();