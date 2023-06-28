using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        AdsDAO adsdao = new AdsDAO();
        public GeneralDTO GetAllPost()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPost();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.PopularPost = dao.GetPopularPost();
            dto.MostViewedPost = dao.GetMostViewedPost();
            dto.Videos = dao.GetVideos();
            dto.Adslist = adsdao.GetAdsList();
            return dto;
        }

        public GeneralDTO GetAPostDetailPageItemWithID(int ID)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAdsList();
            dto.PostDetail = dao.GetPostDetail(ID);
            return dto;
        }
        CategoryDAO categorydao = new CategoryDAO();
        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAdsList();
            if(categoryName=="video")
            {
                dto.Videos = dao.GetAllVideos();
                dto.CategoryName = "video";
            }
            else
            {
                List<CategoryDTO> categorylist = categorydao.GetCategoryList();
                int categoryID = 0;
                foreach (var item in categorylist)
                {
                    if(categoryName==SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dto.CategoryPostList = dao.GetCategoryPostList(categoryID);
            }
            return dto;
        }
        AddressDAO addressdao = new AddressDAO();
        public GeneralDTO GetContactPageItems()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAdsList();
            dto.Address = addressdao.GetAddresses().First();
            return dto;

        }

        public GeneralDTO GetSearchPosts(string searchText)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAdsList();
            dto.CategoryPostList = dao.GetSearchPost(searchText);
            dto.SearchText = searchText;
            return dto;
        }
    }
}
