const { spawn } = require('child_process');
const { promisify } = require('util');
const { readFile, writeFile, exists } = require('fs');
const fkill = require('fkill');
const { resolve } = require('path');

const readFilePromise = promisify(readFile);
const writeFilePromise = promisify(writeFile);
const existsPromise = promisify(exists);

const cwd = process.cwd();

(async () => {
    if (process.argv.length <= 2) {
        console.log('Usage: node phandler.js <app> [<arg>, ..]')
        process.exit(1);
    }

    const app = process.argv[2];
    const args = process.argv.slice(3);

    const appPidPath = resolve(cwd, `${app}.pid`);

    let pid = 0;

    if (await existsPromise(appPidPath)) {
        pid = +(await readFilePromise(appPidPath, 'utf8'));
        try { fkill(pid); } catch {}
    }

    const proc = spawn(app, args, { cwd });
    proc.stdout.on('data', (data) => {
        console.log(data.toString());
    });
    proc.stderr.on('data', (data) => {
        console.error(data.toString());
    });

    pid = proc.pid.toString();
    await writeFilePromise(appPidPath, pid, 'utf8');
})();