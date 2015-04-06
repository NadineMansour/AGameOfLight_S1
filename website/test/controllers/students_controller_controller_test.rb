require 'test_helper'

class StudentsControllerTest < ActionController::TestCase
include Devise::TestHelpers


def setup
@request.env["devise.mapping"] = Devise.mappings[:student]
sign_in students(:one)
end
# test "the truth" do
# assert true
# end

test "should get subjects of school" do
# start the request
get( :show_my_school_subjects)
assert_response :success
assert_not_nil assigns(:subjects)
# we have only 1 subject of their school
assert_equal 1, assigns(:subjects).count
assert_template :show_my_school_subjects
assert_template layout: "layouts/application"
end

end