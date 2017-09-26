using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtension
    {

        /// <summary>
        /// 上传单一图片
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="limit_width">限制的宽度</param>
        /// <param name="limit_height">限制的高度</param>
        /// <returns></returns>
        public static MvcHtmlString UploadSingleImage<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, int limit_width, int limit_height, string control_id)
        {
            //string name = ExpressionHelper.GetExpressionText(expression);
            string name = control_id;

            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;

            string initData = String.Empty;
            if (data != null)
            {
                initData = Convert.ToString(data);
            }


            string upload_url = "/Upload/ImageUploadSingle";


            string button_id = "image_choose_" + name;

            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("<script>KindEditor.ready(function(K){var editor=K.editor({uploadJson:'" + upload_url + "',extraFileUploadParams:{img_width:" + limit_width + ",img_height:" + limit_height + "}});K('#" + button_id + "').click(function(){editor.loadPlugin('image',function(){editor.plugin.imageDialog({showRemote:false,clickFn:function(url,title,width,height,border,align){K('#" + name + "').val(url);editor.hideDialog()}})})})});</script>");


            strBuilder.Append("<input data-val=\"true\" data-val-required=\"必填\" name=\"" + name + "\" type=\"text\" id=\"" + name + "\" value=\"" + initData + "\" />");
            strBuilder.Append("<input type=\"button\" id=\"" + button_id + "\" value=\"选择图片\" />");
            strBuilder.Append("图片尺寸：" + limit_width + "×" + limit_height + "像素");



            /*
            <input type="text" id="image_input" value="" />
            <input type="button" id="image_choose" value="选择图片" />图片尺寸：1200×402像素
            */
            return MvcHtmlString.Create(strBuilder.ToString());

        }


        /// <summary>
        /// 上传单一图片
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="limit_width">限制的宽度</param>
        /// <param name="limit_height">限制的高度</param>
        /// <returns></returns>
        public static MvcHtmlString UploadSingleImage<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, int limit_width, int limit_height)
        {
            string name = ExpressionHelper.GetExpressionText(expression);

            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;

            string initData = String.Empty;
            if (data != null)
            {
                initData = Convert.ToString(data);
            }


            string upload_url = "/Upload/ImageUploadSingle";


            string button_id = "image_choose_" + name;

            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("<script>KindEditor.ready(function(K){var editor=K.editor({uploadJson:'" + upload_url + "',extraFileUploadParams:{img_width:" + limit_width + ",img_height:" + limit_height + "}});K('#" + button_id + "').click(function(){editor.loadPlugin('image',function(){editor.plugin.imageDialog({showRemote:false,clickFn:function(url,title,width,height,border,align){K('#" + name + "').val(url);editor.hideDialog()}})})})});</script>");


            strBuilder.Append("<input data-val=\"true\" data-val-required=\"必填\" name=\"" + name + "\" type=\"text\" id=\"" + name + "\" value=\"" + initData + "\" />");
            strBuilder.Append("<input type=\"button\" id=\"" + button_id + "\" value=\"选择图片\" />");
            strBuilder.Append("图片尺寸：" + limit_width + "×" + limit_height + "像素");



            /*
            <input type="text" id="image_input" value="" />
            <input type="button" id="image_choose" value="选择图片" />图片尺寸：1200×402像素
            */
            return MvcHtmlString.Create(strBuilder.ToString());

        }

    }
}
