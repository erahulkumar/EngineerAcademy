using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categorydao = new CategoryDAO();
        SocialMediaDAO socialmediadao = new SocialMediaDAO();
        FavDAO favdao = new FavDAO();
        MetaDAO meatdao = new MetaDAO();
        AddressDAO addressdao = new AddressDAO();
        PostDAO postdao = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categorydao.GetCategoryList();
            List<SocialMediaDTO> socialmedialist = new List<SocialMediaDTO>();
            socialmedialist = socialmediadao.GetSocialMedia();
            dto.Facebook = socialmedialist.First(x => x.Link.Contains("facebook"));
            dto.Twitter = socialmedialist.First(x => x.Link.Contains("twitter"));
            dto.Instagram = socialmedialist.First(x => x.Link.Contains("instagram"));
            dto.Youtube = socialmedialist.First(x => x.Link.Contains("youtube"));
            dto.Github = socialmedialist.First(x => x.Link.Contains("github"));
            dto.Linkedin = socialmedialist.First(x => x.Link.Contains("linkedin"));
            dto.FavDTO = favdao.GetFav();
            dto.Metalist = meatdao.GetMetaData();
            List<AddressDTO> addresslist = addressdao.GetAddresses();
            dto.AddressList= addresslist;
            dto.HotNews = postdao.GetHotNews();

            return dto;
        }
    }
}
