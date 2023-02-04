using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeenGarden.Business.Utilities
{
    public class DecodeTokenUtil
    {
        private JwtSecurityTokenHandler _tokenHandler;

        public DecodeTokenUtil()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public int Decode(string token, string nameClaim)
        {
            var claim = _tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(selector => selector.Type.ToString().Equals(nameClaim));
            if (claim != null)
            {
                return Int32.Parse(claim.Value);
            }
            return 0;
        }

        public  Guid DecodeID(string token, string nameClaim)
        {
            var claim = _tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(selector => selector.Type.ToString().Equals(nameClaim));
            if (claim != null)
            {
                return Guid.Parse(claim.Value);
            }
            return new Guid();
        }
    }
}
