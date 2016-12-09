SystemJS.config({
    paths: {
        "npm:": "jspm_packages/npm/",
        "github:": "jspm_packages/github/",
        "app/": "src/"
    },
    browserConfig: {
        "baseURL": "/"
    },
    packages: {
        "app": {
            "main": "main.js"
        }
    }
});

SystemJS.config({
    packageConfigPaths: [
        "npm:@*/*.json",
        "npm:*.json",
        "github:*/*.json"
    ],
    map: {
        "assert": "npm:jspm-nodelibs-assert@0.2.0",
        "aurelia-animator-css": "npm:aurelia-animator-css@1.0.1",
        "aurelia-binding": "npm:aurelia-binding@1.1.0",
        "aurelia-bootstrapper": "npm:aurelia-bootstrapper@2.0.0",
        "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.2.0",
        "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.1",
        "aurelia-framework": "npm:aurelia-framework@1.0.7",
        "aurelia-history": "npm:aurelia-history@1.0.0",
        "aurelia-history-browser": "npm:aurelia-history-browser@1.0.0",
        "aurelia-loader": "npm:aurelia-loader@1.0.0",
        "aurelia-loader-default": "npm:aurelia-loader-default@1.0.0",
        "aurelia-logging": "npm:aurelia-logging@1.2.0",
        "aurelia-logging-console": "npm:aurelia-logging-console@1.0.0",
        "aurelia-metadata": "npm:aurelia-metadata@1.0.2",
        "aurelia-pal": "npm:aurelia-pal@1.1.1",
        "aurelia-pal-browser": "npm:aurelia-pal-browser@1.1.0",
        "aurelia-path": "npm:aurelia-path@1.1.1",
        "aurelia-polyfills": "npm:aurelia-polyfills@1.1.1",
        "aurelia-route-recognizer": "npm:aurelia-route-recognizer@1.1.0",
        "aurelia-router": "npm:aurelia-router@1.1.0",
        "aurelia-task-queue": "npm:aurelia-task-queue@1.1.0",
        "aurelia-templating": "npm:aurelia-templating@1.1.4",
        "aurelia-templating-binding": "npm:aurelia-templating-binding@1.0.0",
        "aurelia-templating-resources": "npm:aurelia-templating-resources@1.1.1",
        "aurelia-templating-router": "npm:aurelia-templating-router@1.0.1",
        "buffer": "npm:jspm-nodelibs-buffer@0.2.1",
        "child_process": "npm:jspm-nodelibs-child_process@0.2.0",
        "constants": "npm:jspm-nodelibs-constants@0.2.0",
        "crypto": "npm:jspm-nodelibs-crypto@0.2.0",
        "es6-shim": "github:es-shims/es6-shim@0.35.2",
        "events": "npm:jspm-nodelibs-events@0.2.0",
        "fs": "npm:jspm-nodelibs-fs@0.2.0",
        "http": "npm:jspm-nodelibs-http@0.2.0",
        "https": "npm:jspm-nodelibs-https@0.2.1",
        "os": "npm:jspm-nodelibs-os@0.2.0",
        "path": "npm:jspm-nodelibs-path@0.2.1",
        "process": "npm:jspm-nodelibs-process@0.2.0",
        "servicestack-client": "npm:servicestack-client@0.0.17",
        "stream": "npm:jspm-nodelibs-stream@0.2.0",
        "string_decoder": "npm:jspm-nodelibs-string_decoder@0.2.0",
        "text": "github:systemjs/plugin-text@0.0.9",
        "url": "npm:jspm-nodelibs-url@0.2.0",
        "util": "npm:jspm-nodelibs-util@0.2.1",
        "vm": "npm:jspm-nodelibs-vm@0.2.0",
        "zlib": "npm:jspm-nodelibs-zlib@0.2.2"
    },
    packages: {
        "npm:servicestack-client@0.0.17": {
            "map": {
                "isomorphic-fetch": "npm:isomorphic-fetch@2.2.1",
                "es6-shim": "npm:es6-shim@0.35.2"
            }
        },
        "npm:isomorphic-fetch@2.2.1": {
            "map": {
                "node-fetch": "npm:node-fetch@1.6.3",
                "whatwg-fetch": "npm:whatwg-fetch@2.0.1"
            }
        },
        "npm:jspm-nodelibs-buffer@0.2.1": {
            "map": {
                "buffer": "npm:buffer@4.9.1"
            }
        },
        "npm:node-fetch@1.6.3": {
            "map": {
                "encoding": "npm:encoding@0.1.12",
                "is-stream": "npm:is-stream@1.1.0"
            }
        },
        "npm:buffer@4.9.1": {
            "map": {
                "ieee754": "npm:ieee754@1.1.8",
                "base64-js": "npm:base64-js@1.2.0",
                "isarray": "npm:isarray@1.0.0"
            }
        },
        "npm:encoding@0.1.12": {
            "map": {
                "iconv-lite": "npm:iconv-lite@0.4.15"
            }
        },
        "npm:jspm-nodelibs-http@0.2.0": {
            "map": {
                "http-browserify": "npm:stream-http@2.5.0"
            }
        },
        "npm:jspm-nodelibs-url@0.2.0": {
            "map": {
                "url-browserify": "npm:url@0.11.0"
            }
        },
        "npm:jspm-nodelibs-os@0.2.0": {
            "map": {
                "os-browserify": "npm:os-browserify@0.2.1"
            }
        },
        "npm:jspm-nodelibs-crypto@0.2.0": {
            "map": {
                "crypto-browserify": "npm:crypto-browserify@3.11.0"
            }
        },
        "npm:jspm-nodelibs-zlib@0.2.2": {
            "map": {
                "browserify-zlib": "npm:browserify-zlib@0.1.4"
            }
        },
        "npm:jspm-nodelibs-stream@0.2.0": {
            "map": {
                "stream-browserify": "npm:stream-browserify@2.0.1"
            }
        },
        "npm:stream-http@2.5.0": {
            "map": {
                "inherits": "npm:inherits@2.0.3",
                "xtend": "npm:xtend@4.0.1",
                "builtin-status-codes": "npm:builtin-status-codes@2.0.0",
                "readable-stream": "npm:readable-stream@2.2.2",
                "to-arraybuffer": "npm:to-arraybuffer@1.0.1"
            }
        },
        "npm:stream-browserify@2.0.1": {
            "map": {
                "inherits": "npm:inherits@2.0.3",
                "readable-stream": "npm:readable-stream@2.2.2"
            }
        },
        "npm:crypto-browserify@3.11.0": {
            "map": {
                "inherits": "npm:inherits@2.0.3",
                "public-encrypt": "npm:public-encrypt@4.0.0",
                "diffie-hellman": "npm:diffie-hellman@5.0.2",
                "browserify-cipher": "npm:browserify-cipher@1.0.0",
                "create-hash": "npm:create-hash@1.1.2",
                "randombytes": "npm:randombytes@2.0.3",
                "create-hmac": "npm:create-hmac@1.1.4",
                "pbkdf2": "npm:pbkdf2@3.0.9",
                "create-ecdh": "npm:create-ecdh@4.0.0",
                "browserify-sign": "npm:browserify-sign@4.0.0"
            }
        },
        "npm:browserify-zlib@0.1.4": {
            "map": {
                "readable-stream": "npm:readable-stream@2.2.2",
                "pako": "npm:pako@0.2.9"
            }
        },
        "npm:url@0.11.0": {
            "map": {
                "punycode": "npm:punycode@1.3.2",
                "querystring": "npm:querystring@0.2.0"
            }
        },
        "npm:readable-stream@2.2.2": {
            "map": {
                "isarray": "npm:isarray@1.0.0",
                "inherits": "npm:inherits@2.0.3",
                "string_decoder": "npm:string_decoder@0.10.31",
                "buffer-shims": "npm:buffer-shims@1.0.0",
                "util-deprecate": "npm:util-deprecate@1.0.2",
                "process-nextick-args": "npm:process-nextick-args@1.0.7",
                "core-util-is": "npm:core-util-is@1.0.2"
            }
        },
        "npm:public-encrypt@4.0.0": {
            "map": {
                "create-hash": "npm:create-hash@1.1.2",
                "randombytes": "npm:randombytes@2.0.3",
                "browserify-rsa": "npm:browserify-rsa@4.0.1",
                "bn.js": "npm:bn.js@4.11.6",
                "parse-asn1": "npm:parse-asn1@5.0.0"
            }
        },
        "npm:diffie-hellman@5.0.2": {
            "map": {
                "randombytes": "npm:randombytes@2.0.3",
                "bn.js": "npm:bn.js@4.11.6",
                "miller-rabin": "npm:miller-rabin@4.0.0"
            }
        },
        "npm:create-hash@1.1.2": {
            "map": {
                "inherits": "npm:inherits@2.0.3",
                "sha.js": "npm:sha.js@2.4.8",
                "cipher-base": "npm:cipher-base@1.0.3",
                "ripemd160": "npm:ripemd160@1.0.1"
            }
        },
        "npm:create-hmac@1.1.4": {
            "map": {
                "create-hash": "npm:create-hash@1.1.2",
                "inherits": "npm:inherits@2.0.3"
            }
        },
        "npm:pbkdf2@3.0.9": {
            "map": {
                "create-hmac": "npm:create-hmac@1.1.4"
            }
        },
        "npm:browserify-sign@4.0.0": {
            "map": {
                "create-hash": "npm:create-hash@1.1.2",
                "create-hmac": "npm:create-hmac@1.1.4",
                "inherits": "npm:inherits@2.0.3",
                "browserify-rsa": "npm:browserify-rsa@4.0.1",
                "bn.js": "npm:bn.js@4.11.6",
                "parse-asn1": "npm:parse-asn1@5.0.0",
                "elliptic": "npm:elliptic@6.3.2"
            }
        },
        "npm:jspm-nodelibs-string_decoder@0.2.0": {
            "map": {
                "string_decoder-browserify": "npm:string_decoder@0.10.31"
            }
        },
        "npm:browserify-cipher@1.0.0": {
            "map": {
                "browserify-des": "npm:browserify-des@1.0.0",
                "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
                "browserify-aes": "npm:browserify-aes@1.0.6"
            }
        },
        "npm:create-ecdh@4.0.0": {
            "map": {
                "bn.js": "npm:bn.js@4.11.6",
                "elliptic": "npm:elliptic@6.3.2"
            }
        },
        "npm:browserify-des@1.0.0": {
            "map": {
                "cipher-base": "npm:cipher-base@1.0.3",
                "inherits": "npm:inherits@2.0.3",
                "des.js": "npm:des.js@1.0.0"
            }
        },
        "npm:browserify-rsa@4.0.1": {
            "map": {
                "bn.js": "npm:bn.js@4.11.6",
                "randombytes": "npm:randombytes@2.0.3"
            }
        },
        "npm:miller-rabin@4.0.0": {
            "map": {
                "bn.js": "npm:bn.js@4.11.6",
                "brorand": "npm:brorand@1.0.6"
            }
        },
        "npm:parse-asn1@5.0.0": {
            "map": {
                "browserify-aes": "npm:browserify-aes@1.0.6",
                "create-hash": "npm:create-hash@1.1.2",
                "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
                "pbkdf2": "npm:pbkdf2@3.0.9",
                "asn1.js": "npm:asn1.js@4.9.0"
            }
        },
        "npm:sha.js@2.4.8": {
            "map": {
                "inherits": "npm:inherits@2.0.3"
            }
        },
        "npm:evp_bytestokey@1.0.0": {
            "map": {
                "create-hash": "npm:create-hash@1.1.2"
            }
        },
        "npm:elliptic@6.3.2": {
            "map": {
                "bn.js": "npm:bn.js@4.11.6",
                "inherits": "npm:inherits@2.0.3",
                "brorand": "npm:brorand@1.0.6",
                "hash.js": "npm:hash.js@1.0.3"
            }
        },
        "npm:browserify-aes@1.0.6": {
            "map": {
                "cipher-base": "npm:cipher-base@1.0.3",
                "create-hash": "npm:create-hash@1.1.2",
                "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
                "inherits": "npm:inherits@2.0.3",
                "buffer-xor": "npm:buffer-xor@1.0.3"
            }
        },
        "npm:cipher-base@1.0.3": {
            "map": {
                "inherits": "npm:inherits@2.0.3"
            }
        },
        "npm:des.js@1.0.0": {
            "map": {
                "inherits": "npm:inherits@2.0.3",
                "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
            }
        },
        "npm:hash.js@1.0.3": {
            "map": {
                "inherits": "npm:inherits@2.0.3"
            }
        },
        "npm:asn1.js@4.9.0": {
            "map": {
                "bn.js": "npm:bn.js@4.11.6",
                "inherits": "npm:inherits@2.0.3",
                "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
            }
        }
    }
});
