import { mask } from "vue-the-mask";
export default {
  directives: { mask },
  data() {
    return {
      maskName: {
        mask:
          "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY",
        tokens: {
          Y: {
            pattern: /[А-ЩЬЮЯҐЄІЇа-щьюяґєії-\s']/
          }
        }
      },
      maskLogin: {
        mask:
          "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY",
        tokens: {
          Y: {
            pattern: /^[a-zA-Z0-9_.-]$/
          }
        }
      },
      maskDeclarationNumber: {
        mask: "YYYY-YYYY-YYYY",
        tokens: {
          Y: {
            pattern: /[А-ЩЬЮЯҐЄІЇа-щьюяґєіїa-zA-Z0-9]/
          }
        }
      },
      mask4x4: {
        mask: "YYYY-YYYY-YYYY-YYYY",
        tokens: {
          Y: {
            pattern: /[А-ЩЬЮЯҐЄІЇа-щьюяґєіїa-zA-Z0-9]/
          }
        }
      },
      mask4x4Eng: {
        mask: "YYYY-YYYY-YYYY-YYYY",
        tokens: {
          Y: {
            pattern: /[a-zA-Z0-9]/
          }
        }
      },
      maskPassport: {
        mask: "YYNNNNNN",
        tokens: {
          Y: {
            pattern: /[А-ЩЬЮЯҐЄІЇ-\s']/
          },
          N: {
            pattern: /[0-9]/
          }
        }
      },
      maskNationalId: {
        mask: "NNNNNNNNN",
        tokens: {
          N: {
            pattern: /[0-9]/
          }
        }
      },
      maskBirthCertificate: {
        mask: "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY",
        tokens: {
          Y: {
            pattern: /[?!ЫЪЭЁыъэё@%&$^#`~:,.*|}{?!A-ZА-ЯҐЇІЄ0-9№\\/()-]/
          }
        }
      },
      maskPassword: {
        mask:
          "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY",
        tokens: {
          Y: {
            pattern: /[a-zA-Z0-9$@$!%*?&^-_. +]/
          }
        }
      },
      maskPhone: {
        mask: "+380(NN) NNN-NN-NN",
        tokens: {
          N: {
            pattern: /[0-9]/
          }
        }
      },
      emptyMask255: {
        mask: '*'.repeat(255),
        tokens: {
          '*': { pattern: /./ }
        }
      },
      
      maskGuid: {
        mask: "YYYYYYYY-YYYY-YYYY-YYYY-YYYYYYYYYYYY",
        tokens: {
          Y: {
            pattern: /[a-zA-Z0-9]/
          }
        }
      }
    };
  },
  methods: {
    capitalizeFirstLetter(string) {
      return string.charAt(0).toUpperCase() + string.slice(1);
    }
  }
};
