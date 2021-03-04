//using Csla;
//using System;

//namespace CslaModelTemplates.Dal
//{
//    /// <summary>
//    /// The base criteria object for sorted and paginated lists.
//    /// </summary>
//    [Serializable]
//    public class PaginatedCriteria : CriteriaBase<PaginatedCriteria>
//    {
//        /// <summary>
//        /// The number of the list items on a page.
//        /// 0 means that the list is not paginated.
//        /// </summary>
//        public int PageSize { get; set; }

//        /// <summary>
//        /// The actual number of the list page.
//        /// The page numbers start from 1.
//        /// </summary>
//        public int PageNumber { get; set; }

//        /// <summary>
//        /// The name of the column the list is sorted by.
//        /// </summary>
//        public string SortColumn { get; set; }

//        /// <summary>
//        /// The direction of the sorting.
//        /// </summary>
//        public SortDirection SortDirection { get; set; }
//    }
//}
