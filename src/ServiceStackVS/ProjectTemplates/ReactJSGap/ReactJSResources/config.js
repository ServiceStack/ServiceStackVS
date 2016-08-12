System.config({
    baseURL: "/",
    defaultJSExtensions: true,
    transpiler: false,
    paths: {
        "github:*": "jspm_packages/github/*",
        "npm:*": "jspm_packages/npm/*"
    },

    map: {
        "bootstrap.native": "npm:bootstrap.native@1.0.3",
        "es6-shim": "github:es-shims/es6-shim@0.35.1",
        "jspm": "npm:jspm@0.16.42",
        "react": "npm:react@15.3.0",
        "react-dom": "npm:react-dom@15.3.0",
        "servicestack-client": "npm:servicestack-client@0.0.10",
        "github:jspm/nodelibs-assert@0.1.0": {
            "assert": "npm:assert@1.4.1"
        },
        "github:jspm/nodelibs-buffer@0.1.0": {
            "buffer": "npm:buffer@3.6.0"
        },
        "github:jspm/nodelibs-constants@0.1.0": {
            "constants-browserify": "npm:constants-browserify@0.0.1"
        },
        "github:jspm/nodelibs-crypto@0.1.0": {
            "crypto-browserify": "npm:crypto-browserify@3.11.0"
        },
        "github:jspm/nodelibs-domain@0.1.0": {
            "domain-browser": "npm:domain-browser@1.1.7"
        },
        "github:jspm/nodelibs-events@0.1.1": {
            "events": "npm:events@1.0.2"
        },
        "github:jspm/nodelibs-http@1.7.1": {
            "Base64": "npm:Base64@0.2.1",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "inherits": "npm:inherits@2.0.1",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "github:jspm/nodelibs-https@0.1.0": {
            "https-browserify": "npm:https-browserify@0.0.0"
        },
        "github:jspm/nodelibs-net@0.1.2": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "net": "github:jspm/nodelibs-net@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "timers": "github:jspm/nodelibs-timers@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "github:jspm/nodelibs-os@0.1.0": {
            "os-browserify": "npm:os-browserify@0.1.2"
        },
        "github:jspm/nodelibs-path@0.1.0": {
            "path-browserify": "npm:path-browserify@0.0.0"
        },
        "github:jspm/nodelibs-process@0.1.2": {
            "process": "npm:process@0.11.8"
        },
        "github:jspm/nodelibs-punycode@0.1.0": {
            "punycode": "npm:punycode@1.3.2"
        },
        "github:jspm/nodelibs-querystring@0.1.0": {
            "querystring": "npm:querystring@0.2.0"
        },
        "github:jspm/nodelibs-stream@0.1.0": {
            "stream-browserify": "npm:stream-browserify@1.0.0"
        },
        "github:jspm/nodelibs-string_decoder@0.1.0": {
            "string_decoder": "npm:string_decoder@0.10.31"
        },
        "github:jspm/nodelibs-timers@0.1.0": {
            "timers-browserify": "npm:timers-browserify@1.4.2"
        },
        "github:jspm/nodelibs-tty@0.1.0": {
            "tty-browserify": "npm:tty-browserify@0.0.0"
        },
        "github:jspm/nodelibs-url@0.1.0": {
            "url": "npm:url@0.10.3"
        },
        "github:jspm/nodelibs-util@0.1.0": {
            "util": "npm:util@0.10.3"
        },
        "github:jspm/nodelibs-vm@0.1.0": {
            "vm-browserify": "npm:vm-browserify@0.0.4"
        },
        "github:jspm/nodelibs-zlib@0.1.0": {
            "browserify-zlib": "npm:browserify-zlib@0.1.4"
        },
        "npm:align-text@0.1.4": {
            "kind-of": "npm:kind-of@3.0.4",
            "longest": "npm:longest@1.0.1",
            "repeat-string": "npm:repeat-string@1.5.4"
        },
        "npm:amdefine@1.0.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "module": "github:jspm/nodelibs-module@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:arr-diff@2.0.0": {
            "arr-flatten": "npm:arr-flatten@1.0.1"
        },
        "npm:asap@2.0.4": {
            "domain": "github:jspm/nodelibs-domain@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:asn1.js@4.8.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "bn.js": "npm:bn.js@4.11.6",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "inherits": "npm:inherits@2.0.1",
            "minimalistic-assert": "npm:minimalistic-assert@1.0.0",
            "vm": "github:jspm/nodelibs-vm@0.1.0"
        },
        "npm:asn1@0.1.11": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "sys": "github:jspm/nodelibs-util@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:asn1@0.2.3": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "sys": "github:jspm/nodelibs-util@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:assert-plus@0.1.5": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:assert-plus@0.2.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:assert-plus@1.0.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:assert@1.4.1": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "npm:util@0.10.3"
        },
        "npm:async@0.2.10": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:async@0.9.2": {
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:async@1.5.2": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:aws-sign2@0.5.0": {
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "url": "github:jspm/nodelibs-url@0.1.0"
        },
        "npm:aws-sign2@0.6.0": {
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "url": "github:jspm/nodelibs-url@0.1.0"
        },
        "npm:aws4@1.4.1": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0",
            "url": "github:jspm/nodelibs-url@0.1.0"
        },
        "npm:babel-code-frame@6.11.0": {
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "chalk": "npm:chalk@1.1.3",
            "esutils": "npm:esutils@2.0.2",
            "js-tokens": "npm:js-tokens@2.0.0"
        },
        "npm:babel-core@6.13.2": {
            "babel-code-frame": "npm:babel-code-frame@6.11.0",
            "babel-generator": "npm:babel-generator@6.11.4",
            "babel-helpers": "npm:babel-helpers@6.8.0",
            "babel-messages": "npm:babel-messages@6.8.0",
            "babel-register": "npm:babel-register@6.11.6",
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-template": "npm:babel-template@6.9.0",
            "babel-traverse": "npm:babel-traverse@6.13.0",
            "babel-types": "npm:babel-types@6.13.0",
            "babylon": "npm:babylon@6.8.4",
            "convert-source-map": "npm:convert-source-map@1.3.0",
            "debug": "npm:debug@2.2.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "json5": "npm:json5@0.4.0",
            "lodash": "npm:lodash@4.14.2",
            "minimatch": "npm:minimatch@3.0.3",
            "module": "github:jspm/nodelibs-module@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "path-exists": "npm:path-exists@1.0.0",
            "path-is-absolute": "npm:path-is-absolute@1.0.0",
            "private": "npm:private@0.1.6",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "shebang-regex": "npm:shebang-regex@1.0.0",
            "slash": "npm:slash@1.0.0",
            "source-map": "npm:source-map@0.5.6",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:babel-generator@6.11.4": {
            "babel-messages": "npm:babel-messages@6.8.0",
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-types": "npm:babel-types@6.13.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "detect-indent": "npm:detect-indent@3.0.1",
            "lodash": "npm:lodash@4.14.2",
            "source-map": "npm:source-map@0.5.6"
        },
        "npm:babel-helper-hoist-variables@6.8.0": {
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-types": "npm:babel-types@6.13.0"
        },
        "npm:babel-helpers@6.8.0": {
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-template": "npm:babel-template@6.9.0"
        },
        "npm:babel-messages@6.8.0": {
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:babel-plugin-transform-es2015-modules-systemjs@6.12.0": {
            "babel-helper-hoist-variables": "npm:babel-helper-hoist-variables@6.8.0",
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-template": "npm:babel-template@6.9.0"
        },
        "npm:babel-plugin-transform-global-system-wrapper@0.0.1": {
            "babel-template": "npm:babel-template@6.9.0"
        },
        "npm:babel-register@6.11.6": {
            "babel-core": "npm:babel-core@6.13.2",
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "core-js": "npm:core-js@2.4.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "home-or-tmp": "npm:home-or-tmp@1.0.0",
            "lodash": "npm:lodash@4.14.2",
            "mkdirp": "npm:mkdirp@0.5.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "path-exists": "npm:path-exists@1.0.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "source-map-support": "npm:source-map-support@0.2.10"
        },
        "npm:babel-runtime@6.11.6": {
            "core-js": "npm:core-js@2.4.1",
            "regenerator-runtime": "npm:regenerator-runtime@0.9.5"
        },
        "npm:babel-template@6.9.0": {
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-traverse": "npm:babel-traverse@6.13.0",
            "babel-types": "npm:babel-types@6.13.0",
            "babylon": "npm:babylon@6.8.4",
            "lodash": "npm:lodash@4.14.2"
        },
        "npm:babel-traverse@6.13.0": {
            "babel-code-frame": "npm:babel-code-frame@6.11.0",
            "babel-messages": "npm:babel-messages@6.8.0",
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-types": "npm:babel-types@6.13.0",
            "babylon": "npm:babylon@6.8.4",
            "debug": "npm:debug@2.2.0",
            "globals": "npm:globals@8.18.0",
            "invariant": "npm:invariant@2.2.1",
            "lodash": "npm:lodash@4.14.2",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:babel-types@6.13.0": {
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "babel-traverse": "npm:babel-traverse@6.13.0",
            "esutils": "npm:esutils@2.0.2",
            "lodash": "npm:lodash@4.14.2",
            "to-fast-properties": "npm:to-fast-properties@1.0.2"
        },
        "npm:babylon@6.8.4": {
            "babel-runtime": "npm:babel-runtime@6.11.6",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:bl@0.9.5": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "readable-stream": "npm:readable-stream@1.0.34",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:bl@1.1.2": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "readable-stream": "npm:readable-stream@2.0.6",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:block-stream@0.0.9": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "inherits": "npm:inherits@2.0.1",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0"
        },
        "npm:bluebird@2.10.2": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:bluebird@3.4.1": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:bn.js@4.11.6": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0"
        },
        "npm:boom@2.10.1": {
            "hoek": "npm:hoek@2.16.3",
            "http": "github:jspm/nodelibs-http@1.7.1"
        },
        "npm:bootstrap.native@1.0.3": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:brace-expansion@1.1.6": {
            "balanced-match": "npm:balanced-match@0.4.2",
            "concat-map": "npm:concat-map@0.0.1"
        },
        "npm:braces@1.8.5": {
            "expand-range": "npm:expand-range@1.8.2",
            "preserve": "npm:preserve@0.2.0",
            "repeat-element": "npm:repeat-element@1.1.2"
        },
        "npm:browserify-aes@1.0.6": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "buffer-xor": "npm:buffer-xor@1.0.3",
            "cipher-base": "npm:cipher-base@1.0.2",
            "create-hash": "npm:create-hash@1.1.2",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "inherits": "npm:inherits@2.0.1",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:browserify-cipher@1.0.0": {
            "browserify-aes": "npm:browserify-aes@1.0.6",
            "browserify-des": "npm:browserify-des@1.0.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "evp_bytestokey": "npm:evp_bytestokey@1.0.0"
        },
        "npm:browserify-des@1.0.0": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "cipher-base": "npm:cipher-base@1.0.2",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "des.js": "npm:des.js@1.0.0",
            "inherits": "npm:inherits@2.0.1"
        },
        "npm:browserify-rsa@4.0.1": {
            "bn.js": "npm:bn.js@4.11.6",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "constants": "github:jspm/nodelibs-constants@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "randombytes": "npm:randombytes@2.0.3"
        },
        "npm:browserify-sign@4.0.0": {
            "bn.js": "npm:bn.js@4.11.6",
            "browserify-rsa": "npm:browserify-rsa@4.0.1",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "create-hash": "npm:create-hash@1.1.2",
            "create-hmac": "npm:create-hmac@1.1.4",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "elliptic": "npm:elliptic@6.3.1",
            "inherits": "npm:inherits@2.0.1",
            "parse-asn1": "npm:parse-asn1@5.0.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0"
        },
        "npm:browserify-zlib@0.1.4": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "pako": "npm:pako@0.2.9",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "readable-stream": "npm:readable-stream@2.0.6",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:buffer-crc32@0.2.5": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0"
        },
        "npm:buffer-xor@1.0.3": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:buffer@3.6.0": {
            "base64-js": "npm:base64-js@0.0.8",
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "ieee754": "npm:ieee754@1.1.6",
            "isarray": "npm:isarray@1.0.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:center-align@0.1.3": {
            "align-text": "npm:align-text@0.1.4",
            "lazy-cache": "npm:lazy-cache@1.0.4"
        },
        "npm:chalk@1.1.3": {
            "ansi-styles": "npm:ansi-styles@2.2.1",
            "escape-string-regexp": "npm:escape-string-regexp@1.0.5",
            "has-ansi": "npm:has-ansi@2.0.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "strip-ansi": "npm:strip-ansi@3.0.1",
            "supports-color": "npm:supports-color@2.0.0"
        },
        "npm:cipher-base@1.0.2": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "inherits": "npm:inherits@2.0.1",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "string_decoder": "github:jspm/nodelibs-string_decoder@0.1.0"
        },
        "npm:cliui@2.1.0": {
            "center-align": "npm:center-align@0.1.3",
            "right-align": "npm:right-align@0.1.3",
            "wordwrap": "npm:wordwrap@0.0.2"
        },
        "npm:combined-stream@0.0.7": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "delayed-stream": "npm:delayed-stream@0.0.5",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:combined-stream@1.0.5": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "delayed-stream": "npm:delayed-stream@1.0.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:commander@2.9.0": {
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "graceful-readlink": "npm:graceful-readlink@1.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:constants-browserify@0.0.1": {
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:convert-source-map@1.3.0": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:core-js@1.2.7": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:core-js@2.4.1": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:core-util-is@1.0.2": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0"
        },
        "npm:create-ecdh@4.0.0": {
            "bn.js": "npm:bn.js@4.11.6",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "elliptic": "npm:elliptic@6.3.1"
        },
        "npm:create-hash@1.1.2": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "cipher-base": "npm:cipher-base@1.0.2",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "inherits": "npm:inherits@2.0.1",
            "ripemd160": "npm:ripemd160@1.0.1",
            "sha.js": "npm:sha.js@2.4.5"
        },
        "npm:create-hmac@1.1.4": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "create-hash": "npm:create-hash@1.1.2",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "inherits": "npm:inherits@2.0.1",
            "stream": "github:jspm/nodelibs-stream@0.1.0"
        },
        "npm:cryptiles@2.0.5": {
            "boom": "npm:boom@2.10.1",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0"
        },
        "npm:crypto-browserify@3.11.0": {
            "browserify-cipher": "npm:browserify-cipher@1.0.0",
            "browserify-sign": "npm:browserify-sign@4.0.0",
            "create-ecdh": "npm:create-ecdh@4.0.0",
            "create-hash": "npm:create-hash@1.1.2",
            "create-hmac": "npm:create-hmac@1.1.4",
            "diffie-hellman": "npm:diffie-hellman@5.0.2",
            "inherits": "npm:inherits@2.0.1",
            "pbkdf2": "npm:pbkdf2@3.0.4",
            "public-encrypt": "npm:public-encrypt@4.0.0",
            "randombytes": "npm:randombytes@2.0.3"
        },
        "npm:ctype@0.5.3": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:d@0.1.1": {
            "es5-ext": "npm:es5-ext@0.10.12"
        },
        "npm:dashdash@1.14.0": {
            "assert-plus": "npm:assert-plus@1.0.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:data-uri-to-buffer@0.0.4": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0"
        },
        "npm:debug@2.2.0": {
            "ms": "npm:ms@0.7.1"
        },
        "npm:delayed-stream@0.0.5": {
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:delayed-stream@1.0.0": {
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:des.js@1.0.0": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "inherits": "npm:inherits@2.0.1",
            "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
        },
        "npm:detect-file@0.1.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "fs-exists-sync": "npm:fs-exists-sync@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:detect-indent@3.0.1": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "get-stdin": "npm:get-stdin@4.0.1",
            "minimist": "npm:minimist@1.2.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "repeating": "npm:repeating@1.1.3",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:diffie-hellman@5.0.2": {
            "bn.js": "npm:bn.js@4.11.6",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "miller-rabin": "npm:miller-rabin@4.0.0",
            "randombytes": "npm:randombytes@2.0.3",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:domain-browser@1.1.7": {
            "events": "github:jspm/nodelibs-events@0.1.1"
        },
        "npm:ecc-jsbn@0.1.1": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "jsbn": "npm:jsbn@0.1.0"
        },
        "npm:elliptic@6.3.1": {
            "bn.js": "npm:bn.js@4.11.6",
            "brorand": "npm:brorand@1.0.5",
            "hash.js": "npm:hash.js@1.0.3",
            "inherits": "npm:inherits@2.0.1",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:encoding@0.1.12": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "iconv-lite": "npm:iconv-lite@0.4.13"
        },
        "npm:es5-ext@0.10.12": {
            "es6-iterator": "npm:es6-iterator@2.0.0",
            "es6-symbol": "npm:es6-symbol@3.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:es6-iterator@2.0.0": {
            "d": "npm:d@0.1.1",
            "es5-ext": "npm:es5-ext@0.10.12",
            "es6-symbol": "npm:es6-symbol@3.1.0"
        },
        "npm:es6-shim@0.35.1": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:es6-symbol@3.1.0": {
            "d": "npm:d@0.1.1",
            "es5-ext": "npm:es5-ext@0.10.12"
        },
        "npm:es6-template-strings@2.0.0": {
            "es5-ext": "npm:es5-ext@0.10.12",
            "esniff": "npm:esniff@1.0.0"
        },
        "npm:esniff@1.0.0": {
            "d": "npm:d@0.1.1",
            "es5-ext": "npm:es5-ext@0.10.12"
        },
        "npm:evp_bytestokey@1.0.0": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "create-hash": "npm:create-hash@1.1.2",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0"
        },
        "npm:expand-brackets@0.1.5": {
            "is-posix-bracket": "npm:is-posix-bracket@0.1.1"
        },
        "npm:expand-range@1.8.2": {
            "fill-range": "npm:fill-range@2.2.3"
        },
        "npm:expand-tilde@1.2.2": {
            "os-homedir": "npm:os-homedir@1.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:extglob@0.3.2": {
            "is-extglob": "npm:is-extglob@1.0.0"
        },
        "npm:extsprintf@1.0.2": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:fbjs@0.8.3": {
            "core-js": "npm:core-js@1.2.7",
            "immutable": "npm:immutable@3.8.1",
            "isomorphic-fetch": "npm:isomorphic-fetch@2.2.1",
            "loose-envify": "npm:loose-envify@1.2.0",
            "object-assign": "npm:object-assign@4.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "promise": "npm:promise@7.1.1",
            "ua-parser-js": "npm:ua-parser-js@0.7.10"
        },
        "npm:fd-slicer@1.0.1": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "pend": "npm:pend@1.2.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:fill-range@2.2.3": {
            "is-number": "npm:is-number@2.1.0",
            "isobject": "npm:isobject@2.1.0",
            "randomatic": "npm:randomatic@1.1.5",
            "repeat-element": "npm:repeat-element@1.1.2",
            "repeat-string": "npm:repeat-string@1.5.4"
        },
        "npm:findup-sync@0.4.2": {
            "detect-file": "npm:detect-file@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "is-glob": "npm:is-glob@2.0.1",
            "micromatch": "npm:micromatch@2.3.11",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "resolve-dir": "npm:resolve-dir@0.1.1"
        },
        "npm:fined@1.0.1": {
            "expand-tilde": "npm:expand-tilde@1.2.2",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "lodash.assignwith": "npm:lodash.assignwith@4.1.0",
            "lodash.isarray": "npm:lodash.isarray@4.0.0",
            "lodash.isempty": "npm:lodash.isempty@4.3.1",
            "lodash.isplainobject": "npm:lodash.isplainobject@4.0.5",
            "lodash.isstring": "npm:lodash.isstring@4.0.1",
            "lodash.pick": "npm:lodash.pick@4.3.0",
            "parse-filepath": "npm:parse-filepath@1.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:flagged-respawn@0.3.2": {
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:for-own@0.1.4": {
            "for-in": "npm:for-in@0.1.5"
        },
        "npm:forever-agent@0.5.2": {
            "http": "github:jspm/nodelibs-http@1.7.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "net": "github:jspm/nodelibs-net@0.1.2",
            "tls": "github:jspm/nodelibs-tls@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:forever-agent@0.6.1": {
            "http": "github:jspm/nodelibs-http@1.7.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "net": "github:jspm/nodelibs-net@0.1.2",
            "tls": "github:jspm/nodelibs-tls@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:form-data@0.2.0": {
            "async": "npm:async@0.9.2",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "combined-stream": "npm:combined-stream@0.0.7",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "mime-types": "npm:mime-types@2.0.14",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:form-data@1.0.0-rc4": {
            "async": "npm:async@1.5.2",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "combined-stream": "npm:combined-stream@1.0.5",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "mime-types": "npm:mime-types@2.1.11",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:fs-exists-sync@0.1.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2"
        },
        "npm:fs.realpath@1.0.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:fstream@1.0.10": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "graceful-fs": "npm:graceful-fs@4.1.5",
            "inherits": "npm:inherits@2.0.1",
            "mkdirp": "npm:mkdirp@0.5.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "rimraf": "npm:rimraf@2.5.4",
            "stream": "github:jspm/nodelibs-stream@0.1.0"
        },
        "npm:generate-function@2.0.0": {
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:generate-object-property@1.2.0": {
            "is-property": "npm:is-property@1.0.2"
        },
        "npm:get-stdin@4.0.1": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:getpass@0.1.6": {
            "assert-plus": "npm:assert-plus@1.0.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "tty": "github:jspm/nodelibs-tty@0.1.0"
        },
        "npm:glob-base@0.3.0": {
            "glob-parent": "npm:glob-parent@2.0.0",
            "is-glob": "npm:is-glob@2.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:glob-parent@2.0.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "is-glob": "npm:is-glob@2.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:glob@4.5.3": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "inflight": "npm:inflight@1.0.5",
            "inherits": "npm:inherits@2.0.1",
            "minimatch": "npm:minimatch@2.0.10",
            "once": "npm:once@1.3.3",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:glob@5.0.15": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "inflight": "npm:inflight@1.0.5",
            "inherits": "npm:inherits@2.0.1",
            "minimatch": "npm:minimatch@3.0.3",
            "once": "npm:once@1.3.3",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "path-is-absolute": "npm:path-is-absolute@1.0.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:glob@6.0.4": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "inflight": "npm:inflight@1.0.5",
            "inherits": "npm:inherits@2.0.1",
            "minimatch": "npm:minimatch@3.0.3",
            "once": "npm:once@1.3.3",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "path-is-absolute": "npm:path-is-absolute@1.0.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:glob@7.0.5": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "fs.realpath": "npm:fs.realpath@1.0.0",
            "inflight": "npm:inflight@1.0.5",
            "inherits": "npm:inherits@2.0.1",
            "minimatch": "npm:minimatch@3.0.3",
            "once": "npm:once@1.3.3",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "path-is-absolute": "npm:path-is-absolute@1.0.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:global-modules@0.2.3": {
            "global-prefix": "npm:global-prefix@0.1.4",
            "is-windows": "npm:is-windows@0.2.0",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:global-prefix@0.1.4": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "ini": "npm:ini@1.3.4",
            "is-windows": "npm:is-windows@0.2.0",
            "osenv": "npm:osenv@0.1.3",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "which": "npm:which@1.2.10"
        },
        "npm:globals@8.18.0": {
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:graceful-fs@4.1.5": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "constants": "github:jspm/nodelibs-constants@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:graceful-readlink@1.0.1": {
            "fs": "github:jspm/nodelibs-fs@0.1.2"
        },
        "npm:har-validator@1.8.0": {
            "bluebird": "npm:bluebird@2.10.2",
            "chalk": "npm:chalk@1.1.3",
            "commander": "npm:commander@2.9.0",
            "is-my-json-valid": "npm:is-my-json-valid@2.13.1",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:har-validator@2.0.6": {
            "chalk": "npm:chalk@1.1.3",
            "commander": "npm:commander@2.9.0",
            "is-my-json-valid": "npm:is-my-json-valid@2.13.1",
            "pinkie-promise": "npm:pinkie-promise@2.0.1",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:has-ansi@2.0.0": {
            "ansi-regex": "npm:ansi-regex@2.0.0"
        },
        "npm:hash.js@1.0.3": {
            "inherits": "npm:inherits@2.0.1"
        },
        "npm:hawk@2.3.1": {
            "boom": "npm:boom@2.10.1",
            "cryptiles": "npm:cryptiles@2.0.5",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "hoek": "npm:hoek@2.16.3",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "sntp": "npm:sntp@1.0.9",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2",
            "url": "github:jspm/nodelibs-url@0.1.0"
        },
        "npm:hawk@3.1.3": {
            "boom": "npm:boom@2.10.1",
            "cryptiles": "npm:cryptiles@2.0.5",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "hoek": "npm:hoek@2.16.3",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "sntp": "npm:sntp@1.0.9",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2",
            "url": "github:jspm/nodelibs-url@0.1.0"
        },
        "npm:hoek@2.16.3": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:home-or-tmp@1.0.0": {
            "os-tmpdir": "npm:os-tmpdir@1.0.1",
            "user-home": "npm:user-home@1.1.1"
        },
        "npm:http-signature@0.10.1": {
            "asn1": "npm:asn1@0.1.11",
            "assert-plus": "npm:assert-plus@0.1.5",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "ctype": "npm:ctype@0.5.3",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:http-signature@0.11.0": {
            "asn1": "npm:asn1@0.1.11",
            "assert-plus": "npm:assert-plus@0.1.5",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "ctype": "npm:ctype@0.5.3",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:http-signature@1.1.1": {
            "assert-plus": "npm:assert-plus@0.2.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "jsprim": "npm:jsprim@1.3.0",
            "sshpk": "npm:sshpk@1.9.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:https-browserify@0.0.0": {
            "http": "github:jspm/nodelibs-http@1.7.1"
        },
        "npm:iconv-lite@0.4.13": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "string_decoder": "github:jspm/nodelibs-string_decoder@0.1.0",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:inflight@1.0.5": {
            "once": "npm:once@1.3.3",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "wrappy": "npm:wrappy@1.0.2"
        },
        "npm:inherits@2.0.1": {
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:ini@1.3.4": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:invariant@2.2.1": {
            "loose-envify": "npm:loose-envify@1.2.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:is-absolute@0.2.5": {
            "is-relative": "npm:is-relative@0.2.1",
            "is-windows": "npm:is-windows@0.1.1"
        },
        "npm:is-buffer@1.1.4": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0"
        },
        "npm:is-equal-shallow@0.1.3": {
            "is-primitive": "npm:is-primitive@2.0.0"
        },
        "npm:is-finite@1.0.1": {
            "number-is-nan": "npm:number-is-nan@1.0.0"
        },
        "npm:is-glob@2.0.1": {
            "is-extglob": "npm:is-extglob@1.0.0"
        },
        "npm:is-my-json-valid@2.13.1": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "generate-function": "npm:generate-function@2.0.0",
            "generate-object-property": "npm:generate-object-property@1.2.0",
            "jsonpointer": "npm:jsonpointer@2.0.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "xtend": "npm:xtend@4.0.1"
        },
        "npm:is-number@2.1.0": {
            "kind-of": "npm:kind-of@3.0.4"
        },
        "npm:is-relative@0.2.1": {
            "is-unc-path": "npm:is-unc-path@0.1.1"
        },
        "npm:is-unc-path@0.1.1": {
            "unc-path-regex": "npm:unc-path-regex@0.1.2"
        },
        "npm:is-windows@0.1.1": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:is-windows@0.2.0": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:isexe@1.1.2": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:isobject@2.1.0": {
            "isarray": "npm:isarray@1.0.0"
        },
        "npm:isomorphic-fetch@2.2.1": {
            "node-fetch": "npm:node-fetch@1.6.0",
            "whatwg-fetch": "npm:whatwg-fetch@1.0.0"
        },
        "npm:isstream@0.1.2": {
            "events": "github:jspm/nodelibs-events@0.1.1",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:jodid25519@1.0.2": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "jsbn": "npm:jsbn@0.1.0"
        },
        "npm:json5@0.4.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:jsonpointer@2.0.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0"
        },
        "npm:jspm-github@0.13.16": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "expand-tilde": "npm:expand-tilde@1.2.2",
            "graceful-fs": "npm:graceful-fs@4.1.5",
            "mkdirp": "npm:mkdirp@0.5.1",
            "netrc": "npm:netrc@0.1.4",
            "os": "github:jspm/nodelibs-os@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "request": "npm:request@2.53.0",
            "rimraf": "npm:rimraf@2.3.4",
            "rsvp": "npm:rsvp@3.2.1",
            "semver": "npm:semver@5.3.0",
            "tar": "npm:tar@2.2.1",
            "which": "npm:which@1.2.10",
            "yauzl": "npm:yauzl@2.6.0",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        },
        "npm:jspm-npm@0.26.10": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "glob": "npm:glob@5.0.15",
            "graceful-fs": "npm:graceful-fs@4.1.5",
            "mkdirp": "npm:mkdirp@0.5.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "request": "npm:request@2.58.0",
            "resolve": "npm:resolve@1.1.7",
            "rmdir": "npm:rmdir@1.1.0",
            "rsvp": "npm:rsvp@3.2.1",
            "semver": "npm:semver@5.3.0",
            "systemjs-builder": "npm:systemjs-builder@0.15.26",
            "tar": "npm:tar@1.0.3",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "which": "npm:which@1.2.10",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        },
        "npm:jspm-registry@0.4.1": {
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "graceful-fs": "npm:graceful-fs@4.1.5",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "rimraf": "npm:rimraf@2.5.4",
            "rsvp": "npm:rsvp@3.2.1",
            "semver": "npm:semver@4.3.6"
        },
        "npm:jspm@0.16.42": {
            "chalk": "npm:chalk@1.1.3",
            "core-js": "npm:core-js@1.2.7",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "glob": "npm:glob@6.0.4",
            "graceful-fs": "npm:graceful-fs@4.1.5",
            "jspm-github": "npm:jspm-github@0.13.16",
            "jspm-npm": "npm:jspm-npm@0.26.10",
            "jspm-registry": "npm:jspm-registry@0.4.1",
            "liftoff": "npm:liftoff@2.3.0",
            "minimatch": "npm:minimatch@3.0.3",
            "mkdirp": "npm:mkdirp@0.5.1",
            "ncp": "npm:ncp@2.0.0",
            "os": "github:jspm/nodelibs-os@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "proper-lockfile": "npm:proper-lockfile@1.1.2",
            "request": "npm:request@2.74.0",
            "rimraf": "npm:rimraf@2.5.4",
            "rsvp": "npm:rsvp@3.2.1",
            "semver": "npm:semver@5.3.0",
            "systemjs": "npm:systemjs@0.19.36",
            "systemjs-builder": "npm:systemjs-builder@0.15.26",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2",
            "traceur": "npm:traceur@0.0.105",
            "uglify-js": "npm:uglify-js@2.7.0"
        },
        "npm:jsprim@1.3.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "extsprintf": "npm:extsprintf@1.0.2",
            "json-schema": "npm:json-schema@0.2.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0",
            "verror": "npm:verror@1.3.6"
        },
        "npm:kind-of@3.0.4": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "is-buffer": "npm:is-buffer@1.1.4"
        },
        "npm:lazy-cache@1.0.4": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:liftoff@2.3.0": {
            "events": "github:jspm/nodelibs-events@0.1.1",
            "extend": "npm:extend@3.0.0",
            "findup-sync": "npm:findup-sync@0.4.2",
            "fined": "npm:fined@1.0.1",
            "flagged-respawn": "npm:flagged-respawn@0.3.2",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "lodash.isplainobject": "npm:lodash.isplainobject@4.0.5",
            "lodash.isstring": "npm:lodash.isstring@4.0.1",
            "lodash.mapvalues": "npm:lodash.mapvalues@4.5.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "rechoir": "npm:rechoir@0.6.2",
            "resolve": "npm:resolve@1.1.7",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:lodash.isempty@4.3.1": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:lodash.mapvalues@4.5.1": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:loose-envify@1.2.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "js-tokens": "npm:js-tokens@1.0.3",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:micromatch@2.3.11": {
            "arr-diff": "npm:arr-diff@2.0.0",
            "array-unique": "npm:array-unique@0.2.1",
            "braces": "npm:braces@1.8.5",
            "expand-brackets": "npm:expand-brackets@0.1.5",
            "extglob": "npm:extglob@0.3.2",
            "filename-regex": "npm:filename-regex@2.0.0",
            "is-extglob": "npm:is-extglob@1.0.0",
            "is-glob": "npm:is-glob@2.0.1",
            "kind-of": "npm:kind-of@3.0.4",
            "normalize-path": "npm:normalize-path@2.0.1",
            "object.omit": "npm:object.omit@2.0.0",
            "parse-glob": "npm:parse-glob@3.0.4",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "regex-cache": "npm:regex-cache@0.4.3"
        },
        "npm:miller-rabin@4.0.0": {
            "bn.js": "npm:bn.js@4.11.6",
            "brorand": "npm:brorand@1.0.5"
        },
        "npm:mime-db@1.12.0": {
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:mime-db@1.23.0": {
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:mime-types@2.0.14": {
            "mime-db": "npm:mime-db@1.12.0"
        },
        "npm:mime-types@2.1.11": {
            "mime-db": "npm:mime-db@1.23.0",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:minimatch@2.0.10": {
            "brace-expansion": "npm:brace-expansion@1.1.6",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:minimatch@3.0.3": {
            "brace-expansion": "npm:brace-expansion@1.1.6",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:mkdirp@0.5.1": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "minimist": "npm:minimist@0.0.8",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:ncp@2.0.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:netrc@0.1.4": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:node-fetch@1.6.0": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "encoding": "npm:encoding@0.1.12",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "is-stream": "npm:is-stream@1.1.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        },
        "npm:node-uuid@1.4.7": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0"
        },
        "npm:node.extend@1.0.8": {
            "is": "npm:is@0.2.7",
            "object-keys": "npm:object-keys@0.4.0"
        },
        "npm:node.flow@1.2.3": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "node.extend": "npm:node.extend@1.0.8"
        },
        "npm:oauth-sign@0.6.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0"
        },
        "npm:oauth-sign@0.8.2": {
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0"
        },
        "npm:object.omit@2.0.0": {
            "for-own": "npm:for-own@0.1.4",
            "is-extendable": "npm:is-extendable@0.1.1"
        },
        "npm:once@1.3.3": {
            "wrappy": "npm:wrappy@1.0.2"
        },
        "npm:os-browserify@0.1.2": {
            "os": "github:jspm/nodelibs-os@0.1.0"
        },
        "npm:os-homedir@1.0.1": {
            "os": "github:jspm/nodelibs-os@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:os-tmpdir@1.0.1": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:osenv@0.1.3": {
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "os-homedir": "npm:os-homedir@1.0.1",
            "os-tmpdir": "npm:os-tmpdir@1.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:pako@0.2.9": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:parse-asn1@5.0.0": {
            "asn1.js": "npm:asn1.js@4.8.0",
            "browserify-aes": "npm:browserify-aes@1.0.6",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "create-hash": "npm:create-hash@1.1.2",
            "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
            "pbkdf2": "npm:pbkdf2@3.0.4",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:parse-filepath@1.0.1": {
            "is-absolute": "npm:is-absolute@0.2.5",
            "map-cache": "npm:map-cache@0.2.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "path-root": "npm:path-root@0.1.1"
        },
        "npm:parse-glob@3.0.4": {
            "glob-base": "npm:glob-base@0.3.0",
            "is-dotfile": "npm:is-dotfile@1.0.2",
            "is-extglob": "npm:is-extglob@1.0.0",
            "is-glob": "npm:is-glob@2.0.1"
        },
        "npm:path-browserify@0.0.0": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:path-exists@1.0.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2"
        },
        "npm:path-is-absolute@1.0.0": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:path-root@0.1.1": {
            "path-root-regex": "npm:path-root-regex@0.1.2"
        },
        "npm:pbkdf2@3.0.4": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "create-hmac": "npm:create-hmac@1.1.4",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:pend@1.2.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:pinkie-promise@2.0.1": {
            "pinkie": "npm:pinkie@2.0.4"
        },
        "npm:process-nextick-args@1.0.7": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:process@0.11.8": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "vm": "github:jspm/nodelibs-vm@0.1.0"
        },
        "npm:promise@7.1.1": {
            "asap": "npm:asap@2.0.4",
            "fs": "github:jspm/nodelibs-fs@0.1.2"
        },
        "npm:proper-lockfile@1.1.2": {
            "err-code": "npm:err-code@1.1.1",
            "extend": "npm:extend@3.0.0",
            "graceful-fs": "npm:graceful-fs@4.1.5",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "retry": "npm:retry@0.9.0"
        },
        "npm:public-encrypt@4.0.0": {
            "bn.js": "npm:bn.js@4.11.6",
            "browserify-rsa": "npm:browserify-rsa@4.0.1",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "create-hash": "npm:create-hash@1.1.2",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "parse-asn1": "npm:parse-asn1@5.0.0",
            "randombytes": "npm:randombytes@2.0.3"
        },
        "npm:punycode@1.3.2": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:randomatic@1.1.5": {
            "is-number": "npm:is-number@2.1.0",
            "kind-of": "npm:kind-of@3.0.4"
        },
        "npm:randombytes@2.0.3": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:react-dom@15.3.0": {
            "react": "npm:react@15.3.0"
        },
        "npm:react@15.3.0": {
            "fbjs": "npm:fbjs@0.8.3",
            "loose-envify": "npm:loose-envify@1.2.0",
            "object-assign": "npm:object-assign@4.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:readable-stream@1.0.34": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "core-util-is": "npm:core-util-is@1.0.2",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "inherits": "npm:inherits@2.0.1",
            "isarray": "npm:isarray@0.0.1",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream-browserify": "npm:stream-browserify@1.0.0",
            "string_decoder": "npm:string_decoder@0.10.31"
        },
        "npm:readable-stream@2.0.6": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "core-util-is": "npm:core-util-is@1.0.2",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "inherits": "npm:inherits@2.0.1",
            "isarray": "npm:isarray@1.0.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "process-nextick-args": "npm:process-nextick-args@1.0.7",
            "string_decoder": "npm:string_decoder@0.10.31",
            "util-deprecate": "npm:util-deprecate@1.0.2"
        },
        "npm:rechoir@0.6.2": {
            "path": "github:jspm/nodelibs-path@0.1.0",
            "resolve": "npm:resolve@1.1.7"
        },
        "npm:regenerator-runtime@0.9.5": {
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:regex-cache@0.4.3": {
            "is-equal-shallow": "npm:is-equal-shallow@0.1.3",
            "is-primitive": "npm:is-primitive@2.0.0"
        },
        "npm:repeating@1.1.3": {
            "is-finite": "npm:is-finite@1.0.1",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:request@2.53.0": {
            "aws-sign2": "npm:aws-sign2@0.5.0",
            "bl": "npm:bl@0.9.5",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "caseless": "npm:caseless@0.9.0",
            "combined-stream": "npm:combined-stream@0.0.7",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "forever-agent": "npm:forever-agent@0.5.2",
            "form-data": "npm:form-data@0.2.0",
            "hawk": "npm:hawk@2.3.1",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "http-signature": "npm:http-signature@0.10.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "isstream": "npm:isstream@0.1.2",
            "json-stringify-safe": "npm:json-stringify-safe@5.0.1",
            "mime-types": "npm:mime-types@2.0.14",
            "net": "github:jspm/nodelibs-net@0.1.2",
            "node-uuid": "npm:node-uuid@1.4.7",
            "oauth-sign": "npm:oauth-sign@0.6.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "qs": "npm:qs@2.3.3",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "stringstream": "npm:stringstream@0.0.5",
            "tough-cookie": "npm:tough-cookie@2.3.1",
            "tunnel-agent": "npm:tunnel-agent@0.4.3",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        },
        "npm:request@2.58.0": {
            "aws-sign2": "npm:aws-sign2@0.5.0",
            "bl": "npm:bl@0.9.5",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "caseless": "npm:caseless@0.10.0",
            "combined-stream": "npm:combined-stream@1.0.5",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "extend": "npm:extend@2.0.1",
            "forever-agent": "npm:forever-agent@0.6.1",
            "form-data": "npm:form-data@1.0.0-rc4",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "har-validator": "npm:har-validator@1.8.0",
            "hawk": "npm:hawk@2.3.1",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "http-signature": "npm:http-signature@0.11.0",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "isstream": "npm:isstream@0.1.2",
            "json-stringify-safe": "npm:json-stringify-safe@5.0.1",
            "mime-types": "npm:mime-types@2.0.14",
            "node-uuid": "npm:node-uuid@1.4.7",
            "oauth-sign": "npm:oauth-sign@0.8.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "qs": "npm:qs@3.1.0",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "stringstream": "npm:stringstream@0.0.5",
            "tough-cookie": "npm:tough-cookie@2.3.1",
            "tunnel-agent": "npm:tunnel-agent@0.4.3",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        },
        "npm:request@2.74.0": {
            "aws-sign2": "npm:aws-sign2@0.6.0",
            "aws4": "npm:aws4@1.4.1",
            "bl": "npm:bl@1.1.2",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "caseless": "npm:caseless@0.11.0",
            "combined-stream": "npm:combined-stream@1.0.5",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "extend": "npm:extend@3.0.0",
            "forever-agent": "npm:forever-agent@0.6.1",
            "form-data": "npm:form-data@1.0.0-rc4",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "har-validator": "npm:har-validator@2.0.6",
            "hawk": "npm:hawk@3.1.3",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "http-signature": "npm:http-signature@1.1.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "is-typedarray": "npm:is-typedarray@1.0.0",
            "isstream": "npm:isstream@0.1.2",
            "json-stringify-safe": "npm:json-stringify-safe@5.0.1",
            "mime-types": "npm:mime-types@2.1.11",
            "node-uuid": "npm:node-uuid@1.4.7",
            "oauth-sign": "npm:oauth-sign@0.8.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "qs": "npm:qs@6.2.1",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "stringstream": "npm:stringstream@0.0.5",
            "tough-cookie": "npm:tough-cookie@2.3.1",
            "tunnel-agent": "npm:tunnel-agent@0.4.3",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        },
        "npm:resolve-dir@0.1.1": {
            "expand-tilde": "npm:expand-tilde@1.2.2",
            "global-modules": "npm:global-modules@0.2.3",
            "path": "github:jspm/nodelibs-path@0.1.0"
        },
        "npm:resolve@1.1.7": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:right-align@0.1.3": {
            "align-text": "npm:align-text@0.1.4"
        },
        "npm:rimraf@2.3.4": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "glob": "npm:glob@4.5.3",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:rimraf@2.5.4": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "glob": "npm:glob@7.0.5",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:ripemd160@1.0.1": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:rmdir@1.1.0": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "node.flow": "npm:node.flow@1.2.3"
        },
        "npm:rollup@0.31.2": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "source-map-support": "npm:source-map-support@0.4.2"
        },
        "npm:rsvp@3.2.1": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:semver@4.3.6": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:semver@5.3.0": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:servicestack-client@0.0.10": {
            "es6-shim": "npm:es6-shim@0.35.1",
            "isomorphic-fetch": "npm:isomorphic-fetch@2.2.1"
        },
        "npm:sha.js@2.4.5": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "inherits": "npm:inherits@2.0.1",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:sntp@1.0.9": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "dgram": "github:jspm/nodelibs-dgram@0.1.0",
            "dns": "github:jspm/nodelibs-dns@0.1.0",
            "hoek": "npm:hoek@2.16.3",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:source-map-support@0.2.10": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0",
            "source-map": "npm:source-map@0.1.32"
        },
        "npm:source-map-support@0.4.2": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "child_process": "github:jspm/nodelibs-child_process@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "querystring": "github:jspm/nodelibs-querystring@0.1.0",
            "source-map": "npm:source-map@0.1.32"
        },
        "npm:source-map@0.1.32": {
            "amdefine": "npm:amdefine@1.0.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:source-map@0.5.6": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:sshpk@1.9.2": {
            "asn1": "npm:asn1@0.2.3",
            "assert-plus": "npm:assert-plus@1.0.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "dashdash": "npm:dashdash@1.14.0",
            "ecc-jsbn": "npm:ecc-jsbn@0.1.1",
            "getpass": "npm:getpass@0.1.6",
            "jodid25519": "npm:jodid25519@1.0.2",
            "jsbn": "npm:jsbn@0.1.0",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "tweetnacl": "npm:tweetnacl@0.13.3",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:stream-browserify@1.0.0": {
            "events": "github:jspm/nodelibs-events@0.1.1",
            "inherits": "npm:inherits@2.0.1",
            "readable-stream": "npm:readable-stream@1.0.34"
        },
        "npm:string_decoder@0.10.31": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0"
        },
        "npm:stringstream@0.0.5": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "string_decoder": "github:jspm/nodelibs-string_decoder@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        },
        "npm:strip-ansi@3.0.1": {
            "ansi-regex": "npm:ansi-regex@2.0.0"
        },
        "npm:supports-color@2.0.0": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:systemjs-builder@0.15.26": {
            "babel-core": "npm:babel-core@6.13.2",
            "babel-plugin-transform-es2015-modules-systemjs": "npm:babel-plugin-transform-es2015-modules-systemjs@6.12.0",
            "babel-plugin-transform-global-system-wrapper": "npm:babel-plugin-transform-global-system-wrapper@0.0.1",
            "babel-plugin-transform-system-register": "npm:babel-plugin-transform-system-register@0.0.1",
            "bluebird": "npm:bluebird@3.4.1",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "crypto": "github:jspm/nodelibs-crypto@0.1.0",
            "data-uri-to-buffer": "npm:data-uri-to-buffer@0.0.4",
            "es6-template-strings": "npm:es6-template-strings@2.0.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "glob": "npm:glob@7.0.5",
            "mkdirp": "npm:mkdirp@0.5.1",
            "module": "github:jspm/nodelibs-module@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "rollup": "npm:rollup@0.31.2",
            "source-map": "npm:source-map@0.5.6",
            "systemjs": "npm:systemjs@0.19.36",
            "traceur": "npm:traceur@0.0.105",
            "uglify-js": "npm:uglify-js@2.7.0",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "vm": "github:jspm/nodelibs-vm@0.1.0"
        },
        "npm:systemjs@0.19.36": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2",
            "when": "npm:when@3.7.7"
        },
        "npm:tar@1.0.3": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "block-stream": "npm:block-stream@0.0.9",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "fstream": "npm:fstream@1.0.10",
            "inherits": "npm:inherits@2.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:tar@2.2.1": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "block-stream": "npm:block-stream@0.0.9",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "fstream": "npm:fstream@1.0.10",
            "inherits": "npm:inherits@2.0.1",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:timers-browserify@1.4.2": {
            "process": "npm:process@0.11.8"
        },
        "npm:tough-cookie@2.3.1": {
            "net": "github:jspm/nodelibs-net@0.1.2",
            "punycode": "github:jspm/nodelibs-punycode@0.1.0",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2",
            "url": "github:jspm/nodelibs-url@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:traceur@0.0.105": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "commander": "npm:commander@2.9.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "glob": "npm:glob@5.0.15",
            "module": "github:jspm/nodelibs-module@0.1.0",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "rsvp": "npm:rsvp@3.2.1",
            "semver": "npm:semver@4.3.6",
            "source-map-support": "npm:source-map-support@0.2.10",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2",
            "vm": "github:jspm/nodelibs-vm@0.1.0"
        },
        "npm:tunnel-agent@0.4.3": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "http": "github:jspm/nodelibs-http@1.7.1",
            "https": "github:jspm/nodelibs-https@0.1.0",
            "net": "github:jspm/nodelibs-net@0.1.2",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "tls": "github:jspm/nodelibs-tls@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:ua-parser-js@0.7.10": {
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:uglify-js@2.7.0": {
            "async": "npm:async@0.2.10",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "source-map": "npm:source-map@0.5.6",
            "uglify-to-browserify": "npm:uglify-to-browserify@1.0.2",
            "yargs": "npm:yargs@3.10.0"
        },
        "npm:uglify-to-browserify@1.0.2": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0"
        },
        "npm:url@0.10.3": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "punycode": "npm:punycode@1.3.2",
            "querystring": "npm:querystring@0.2.0",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:user-home@1.1.1": {
            "process": "github:jspm/nodelibs-process@0.1.2",
            "systemjs-json": "github:systemjs/plugin-json@0.1.2"
        },
        "npm:util-deprecate@1.0.2": {
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:util@0.10.3": {
            "inherits": "npm:inherits@2.0.1",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:verror@1.3.6": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "extsprintf": "npm:extsprintf@1.0.2",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "util": "github:jspm/nodelibs-util@0.1.0"
        },
        "npm:vm-browserify@0.0.4": {
            "indexof": "npm:indexof@0.0.1"
        },
        "npm:when@3.7.7": {
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:which@1.2.10": {
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "isexe": "npm:isexe@1.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2"
        },
        "npm:window-size@0.1.0": {
            "process": "github:jspm/nodelibs-process@0.1.2",
            "tty": "github:jspm/nodelibs-tty@0.1.0"
        },
        "npm:yargs@3.10.0": {
            "assert": "github:jspm/nodelibs-assert@0.1.0",
            "camelcase": "npm:camelcase@1.2.1",
            "cliui": "npm:cliui@2.1.0",
            "decamelize": "npm:decamelize@1.2.0",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "path": "github:jspm/nodelibs-path@0.1.0",
            "process": "github:jspm/nodelibs-process@0.1.2",
            "window-size": "npm:window-size@0.1.0"
        },
        "npm:yauzl@2.6.0": {
            "buffer": "github:jspm/nodelibs-buffer@0.1.0",
            "buffer-crc32": "npm:buffer-crc32@0.2.5",
            "events": "github:jspm/nodelibs-events@0.1.1",
            "fd-slicer": "npm:fd-slicer@1.0.1",
            "fs": "github:jspm/nodelibs-fs@0.1.2",
            "stream": "github:jspm/nodelibs-stream@0.1.0",
            "util": "github:jspm/nodelibs-util@0.1.0",
            "zlib": "github:jspm/nodelibs-zlib@0.1.0"
        }
    }
});
