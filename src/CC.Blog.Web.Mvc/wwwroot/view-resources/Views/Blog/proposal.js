(function () {
    $(function () {

        var _blogService = abp.services.app.blog;

        $('#RefreshButton').click(function () {
            refreshProposalList();
        });

        $('.delete-proposal').click(function () {
            var proposalId = $(this).attr("data-proposal-id");
            var proposalTitle = $(this).attr('data-proposal-title');

            deleteArticle(proposalId, proposalTitle);
        });

        function refreshProposalList() {
            location.reload(true); //reload page to see new user!
        }

        function deleteArticle(proposalId, proposalTitle) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'Blog'), proposalTitle),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _blogService.deleteProposal(proposalId).done(function () {
                            refreshProposalList();
                        });
                    }
                }
            );
        }
    });
})();
